using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
  public record Daten(string Irgendwas);

  internal class AnonymeMethoden
  {
    public void Demo()
    {
      List<Daten> list = new List<Daten>();//...

      list.Sort(new Comparison<Daten>(VergleicheNachIrgendwas));
      list.Sort(VergleicheNachIrgendwas);

      int richtung = -1;
      list.Sort(delegate (Daten d1, Daten d2)  // anonyme Methode (mit Closure "richtung")
      {
        return richtung * 42;
      });

      // analog mit Lambda-Ausdruck
      list.Sort((d1, d2) =>
      {
        return richtung * 42;
      });

      list.Sort((d1, d2) => richtung * 42);

    }

    private int VergleicheNachIrgendwas(Daten d1, Daten d2)
    {
      return 42;
    }
  }
}
