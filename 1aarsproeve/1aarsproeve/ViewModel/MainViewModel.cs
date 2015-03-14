using System;
using System.Collections.Generic;
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
        public MainViewModel()
        {
            var kultur = CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("da-DK");
            int ugeNummer = kultur.Calendar.GetWeekOfYear(DateTime.Today, DateTimeFormatInfo.GetInstance(kultur).CalendarWeekRule, DateTimeFormatInfo.GetInstance(kultur).FirstDayOfWeek);

            Ugenummer = "Uge: " + ugeNummer;
        }
    }
}
