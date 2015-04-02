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
    class HovedViewModel
    {
        public ApplicationDataContainer Setting { get; set; }
        public string Brugernavn { get; set; }
        public ICommand LogUdCommand { get; set; }
        public HovedViewModel()
        {
            Setting = ApplicationData.Current.LocalSettings;
            Setting.Values["Brugernavn"] = "Daniel Winther";

            Brugernavn = (string)Setting.Values["Brugernavn"];

            LogUdCommand = new RelayCommand(LogUd);
        }

        public void LogUd()
        {
            Setting.Values.Remove("Brugernavn");

            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Login));
        }
    }
}
