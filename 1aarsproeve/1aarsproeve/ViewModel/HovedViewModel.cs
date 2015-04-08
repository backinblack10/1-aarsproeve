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
    /// <summary>
    /// DataContext klasse til Views: Hovedmenu, SkrivBesked
    /// </summary>
    class HovedViewModel
    {
        /// <summary>
        /// Gør det muligt at gemme værdier i local storage
        /// </summary>
        public ApplicationDataContainer Setting { get; set; }
        /// <summary>
        /// Brugernavn property
        /// </summary>
        public string Brugernavn { get; set; }
        /// <summary>
        /// Logger brugeren ud
        /// </summary>
        public ICommand LogUdCommand { get; set; }
        /// <summary>
        /// Constructor for HovedViewModel
        /// </summary>
        public HovedViewModel()
        {
            Setting = ApplicationData.Current.LocalSettings;
            Setting.Values["Brugernavn"] = "Daniel Winther";

            Brugernavn = (string)Setting.Values["Brugernavn"];

            LogUdCommand = new RelayCommand(LogUd);
        }
        /// <summary>
        /// Logger brugeren ud
        /// </summary>
        public void LogUd()
        {
            Setting.Values.Remove("Brugernavn");

            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Login));
        }
    }
}
