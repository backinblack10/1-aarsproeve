using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace _1aarsproeve.ViewModel
{
    class MainViewModel
    {
        public string Ugenummer { get; set; }
        public ObservableCollection<Ansatte> AnsatteCollection { get; set; }
        public MainViewModel()
        {
            var kultur = CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("da-DK");
            int ugeNummer = kultur.Calendar.GetWeekOfYear(DateTime.Today, DateTimeFormatInfo.GetInstance(kultur).CalendarWeekRule, DateTimeFormatInfo.GetInstance(kultur).FirstDayOfWeek);
            Ugenummer = "Uge: " + ugeNummer;

            AnsatteCollection = new ObservableCollection<Ansatte>()
            {
                new Ansatte {Navn = "Daniel Winther", Tid = "16:00 - 22:30"},
                new Ansatte {Navn = "Benjamin Jensen", Tid = "7:00 - 16:50"},
                new Ansatte {Navn = "Jari Larsen", Tid = "8:30 - 17:30"},
                new Ansatte {Navn = "Jacob Balling", Tid = "6:00 - 14:20"},
                new Ansatte {Navn = "Peter Hansen", Tid = "10:00 - 18:00"},
                new Ansatte {Navn = "Mustafa Johansen", Tid = "11:00 - 15:30"}
            };
        }
    }

    class Ansatte
    {
        public string Navn { get; set; }
        public string Tid { get; set; }
    }
}
