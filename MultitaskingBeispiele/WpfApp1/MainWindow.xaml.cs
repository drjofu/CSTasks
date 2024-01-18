using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private long zähler;
    private DispatcherTimer timer;
    private object syncObject = new();

    public MainWindow()
    {
      InitializeComponent();
      timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromMilliseconds(100);
      timer.Tick += Timer_Tick;
      timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
      LBL.Content = zähler;
    }

    private void BTNStart_Click(object sender, RoutedEventArgs e)
    {
      //Inkrementieren();
      Task.Run(Inkrementieren);
    }

    private void Inkrementieren()
    {
      for (long i = 0; i < 100000000; i++)
      {
        //Monitor.Enter(syncObject); // niemals Wertetypen übergeben (Boxing)!!!
        //try
        //{
        //  zähler++;
        //}
        //finally
        //{
        //  Monitor.Exit(syncObject);
        //}

        lock (syncObject)
        {
          zähler++;
        }
      }
    }
  }
}