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

    private async void BTNStart_Click(object sender, RoutedEventArgs e)
    {
      BTNStart.IsEnabled = false;
      //Task.Run(Inkrementieren)
      //  .ContinueWith(t =>
      //  {
      //    LBL.Content = "es geht weiter";
      //    Task.Run(Inkrementieren).ContinueWith(t => BTNStart.IsEnabled = true, TaskScheduler.FromCurrentSynchronizationContext());
      //  }, TaskScheduler.FromCurrentSynchronizationContext());

      await Task.Run(Inkrementieren);
      BTNStart.IsEnabled = true;
    }

    private void Inkrementieren()
    {
      for (int i = 0; i < 5; i++)
      {
        Thread.Sleep(1000);
      }
    }
  }
}
