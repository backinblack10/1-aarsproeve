using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace _1aarsproeve.ViewModel
{
    class HovedViewModel
    {
        public ApplicationDataContainer Setting { get; set; }
        public string Brugernavn { get; set; }
        public HovedViewModel()
        {
            Setting = ApplicationData.Current.LocalSettings;
            Setting.Values["Brugernavn"] = "Daniel Winther";

            Brugernavn = (string)Setting.Values["Brugernavn"];
        }
    }
}
