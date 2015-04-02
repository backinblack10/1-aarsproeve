using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using _1aarsproeve.Common;
using _1aarsproeve.View;

namespace _1aarsproeve.ViewModel
{
    class BrugerViewModel
    {
        public ApplicationDataContainer Setting { get; set; }
        public string Brugernavn { get; set; }

        public ICommand LogIndCommand { get; set; }
        public ICommand LogUdCommand { get; set; }

        public BrugerViewModel()
        {
            Setting = ApplicationData.Current.LocalSettings;
            Setting.Values["Brugernavn"] = "Daniel Winther";

            Brugernavn = (string)Setting.Values["Brugernavn"];

            LogIndCommand = new RelayCommand(LogInd);
            LogUdCommand = new RelayCommand(LogUd);
        }

        private void LogInd()
        {
            Setting = ApplicationData.Current.LocalSettings;
            Setting.Values["Brugernavn"] = "Daniel Winther";

            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Hovedmenu));
        }

        public void LogUd()
        {
            Setting.Values.Remove("Brugernavn");

            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Login));
        }
    }
}
