using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace _1aarsproeve.ViewModel
{
    class HovedmenuViewModel
    {
        public ApplicationDataContainer Setting { get; set; }
        public string Brugernavn { get; set; }

        public ObservableCollection<Besked> BeskedCollection { get; set; }
        public HovedmenuViewModel()
        {
            Setting = ApplicationData.Current.LocalSettings;
            Brugernavn = (string)Setting.Values["Brugernavn"];

            BeskedCollection = new ObservableCollection<Besked>()
            {
                new Besked{Overskrift = "Tester", Forfatter = "Daniel Winther", Dato = new DateTime(2015, 09, 10), Beskrivelse = "Opgøret startede tilsyneladende i indkøbscenteret, hvor et vidne fortæller om, at gerningsmændene truede flere med knive. Så løb de ned i p-kælderen, hvor opgøret ifølge politiet kulminerede med skyderi."},
                new Besked{Overskrift = "Tester", Forfatter = "Daniel Winther", Dato = new DateTime(2015, 09, 10), Beskrivelse = "Opgøret startede tilsyneladende i indkøbscenteret, hvor et vidne fortæller om, at gerningsmændene truede flere med knive. Så løb de ned i p-kælderen, hvor opgøret ifølge politiet kulminerede med skyderi."},
                new Besked{Overskrift = "Tester", Forfatter = "Daniel Winther", Dato = new DateTime(2015, 09, 10), Beskrivelse = "Opgøret startede tilsyneladende i indkøbscenteret, hvor et vidne fortæller om, at gerningsmændene truede flere med knive. Så løb de ned i p-kælderen, hvor opgøret ifølge politiet kulminerede med skyderi."},
            };
        }
    }

    class Besked
    {
        public string Overskrift { get; set; }
        public string Forfatter { get; set; }
        public DateTime Dato { get; set; }
        public string Beskrivelse { get; set; }
    }
}
