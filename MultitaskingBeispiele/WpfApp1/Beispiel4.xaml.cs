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
  /// Interaction logic for Beispiel4.xaml
  /// </summary>
  public partial class Beispiel4 : Window
  {
    public List<Counter> Counters { get; set; } = new List<Counter>();

    public Beispiel4()
    {
      InitializeComponent();

      for (int i = 0; i < 100; i++)
      {
        Counters.Add(new() { Index = i });
      }
      this.DataContext = Counters;

      RunCounters();
    }

    private void RunCounters()
    {
      foreach (var counter in Counters)
      {
        Task.Factory.StartNew(Increment, counter, TaskCreationOptions.LongRunning);
      }
    }

    private async Task Increment(object data)
    {
      var counter = (Counter)data;
      for (int i = 0; i < 10; i++)
      {
        counter.Count++;
        Thread.Sleep(1000);
        //await Task.Delay(1000);
      }
    }
  }
}
