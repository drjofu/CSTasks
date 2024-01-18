using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
  /// <summary>
  /// Interaction logic for Beispiel3.xaml
  /// </summary>
  public partial class Beispiel3 : Window
  {
    public Beispiel3()
    {
      InitializeComponent();
    }

    private CancellationTokenSource cancellationTokenSource;

    private async void BTNStart_Click(object sender, RoutedEventArgs e)
    {
      BTNStart.IsEnabled = false;
      //Task.Run(Inkrementieren)
      //  .ContinueWith(t =>
      //  {
      //    LBL.Content = "es geht weiter";
      //    Task.Run(Inkrementieren).ContinueWith(t => BTNStart.IsEnabled = true, TaskScheduler.FromCurrentSynchronizationContext());
      //  }, TaskScheduler.FromCurrentSynchronizationContext());

      //await Task.Run(Inkrementieren);

      using (cancellationTokenSource = new CancellationTokenSource())
      {

        var ergebnis = await Berechnen();
        LBL.Content = $"Zwischenergebnis: {ergebnis}";
        BTNStop.IsEnabled = true;

        var progress = new Progress<int>(wert => LBL.Content = wert);
        Task t;
        try
        {
          t = Inkrementieren(progress, cancellationTokenSource.Token);
          //await t.ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing | ConfigureAwaitOptions.ContinueOnCapturedContext);
          await t;
        }
        catch (Exception ex)
        {
        }
      }

      BTNStart.IsEnabled = true;
      BTNStop.IsEnabled= false;
    }

    private async Task<int> Berechnen()
    {
      await Task.Delay(2000);
      return 42;
    }

    private Task Inkrementieren(IProgress<int> progress, CancellationToken cancellationToken)
    {
      return Task.Run(async () =>
      {
        for (int i = 0; i < 10; i++)
        {
          if (cancellationToken.IsCancellationRequested)
          {
            //... aufräumen
            // kein return;
          }

          cancellationToken.ThrowIfCancellationRequested();

          //throw new ApplicationException("ohh...");
          //Thread.Sleep(100000);
          await Task.Delay(1000, cancellationToken);
          progress.Report(i);
        }
      });
    }

    private void BTNStop_Click(object sender, RoutedEventArgs e)
    {
      cancellationTokenSource.Cancel();
    }
  }
}
