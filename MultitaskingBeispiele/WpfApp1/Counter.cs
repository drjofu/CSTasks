using System.ComponentModel;

namespace WpfApp1
{
  public class Counter : INotifyPropertyChanged
  {
    public int Index { get; set; }
    private int count;

    public event PropertyChangedEventHandler? PropertyChanged;

    public int Count
    {
      get { return count; }
      set { count = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count))); }
    }

  }

}
