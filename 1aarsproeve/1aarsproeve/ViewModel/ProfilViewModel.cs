using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace _1aarsproeve.ViewModel
{
    class ProfilViewModel
    {
        public ApplicationDataContainer Setting { get; set; }
        public string Brugernavn { get; set; }

        public ProfilViewModel()
        {
            Setting = ApplicationData.Current.LocalSettings;
            Brugernavn = (string)Setting.Values["Brugernavn"];
        }
    }
}
