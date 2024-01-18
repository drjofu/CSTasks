using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
  public record Auftrag(string Artikel, int Anzahl);

  /// <summary>
  /// Interaction logic for ChannelBeispiel.xaml
  /// </summary>
  public partial class ChannelBeispiel : Window
  {
    private readonly Channel<Auftrag> channelAuftragseingang = Channel.CreateUnbounded<Auftrag>();
    private CancellationTokenSource cancellationTokenSource = new();
    private CancellationToken cancellationToken;

    public ChannelBeispiel()
    {
      InitializeComponent();
      cancellationToken = cancellationTokenSource.Token;

      Task.Run(BestellungenBearbeiten);
      Task.Run(BestellungenBearbeiten);
      Task.Run(BestellungenBearbeiten);
    }

    private async void BestellungenBearbeiten()
    {
      //await foreach (var item in channelAuftragseingang.Reader.ReadAllAsync())
      //{
      //  await Task.Delay(5000);
      //  Debug.WriteLine($"Bestellung {item.Anzahl} x {item.Artikel} bearbeitet");
      //}

      while (!cancellationToken.IsCancellationRequested)
      {
        var item = await channelAuftragseingang.Reader.ReadAsync(cancellationToken);
        await Task.Delay(5000);
        Debug.WriteLine($"Bestellung {item.Anzahl} x {item.Artikel} bearbeitet");

      }
      cancellationToken.ThrowIfCancellationRequested();

    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
      Auftrag auftrag = new Auftrag(TBArtikel.Text, (int)SLAnzahl.Value);
      await channelAuftragseingang.Writer.WriteAsync(auftrag, cancellationToken);
      //...
    }
  }
}
