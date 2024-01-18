using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
  /// <summary>
  /// Interaction logic for Beispiel2.xaml
  /// </summary>
  public partial class Beispiel2 : Window
  {
    public Beispiel2()
    {
      InitializeComponent();
    }

    private void BTNStart_Click(object sender, RoutedEventArgs e)
    {
      BTNStart.IsEnabled = false;
      Task.Run(Inkrementieren);
    }

    private void Inkrementieren()
    {
      for (int i = 0; i < 10; i++)
      {
        Thread.Sleep(1000);
        //LBL.Content = i;
        //Dispatcher.Invoke(Ausgeben, i);
        //Dispatcher.BeginInvoke(Ausgeben, i);
        Dispatcher.BeginInvoke(new Action<int>(wert => LBL.Content = wert), i);
        //Dispatcher.BeginInvoke(new Action(() => LBL.Content = i));  // Closure

        //int n = i;
        //Dispatcher.BeginInvoke(new Action(() => Debug.WriteLine($"{n} / {i}")));
      }

      Dispatcher.BeginInvoke(new Action(()=>BTNStart.IsEnabled=true));

    }

    private void Ausgeben(int wert)
    {
      LBL.Content = wert;
    }
  }
}
