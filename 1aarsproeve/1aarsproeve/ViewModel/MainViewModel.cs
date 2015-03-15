using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using _1aarsproeve.Common;

namespace _1aarsproeve.ViewModel
{
    class MainViewModel
    {
        public Brush MandagFarve { get; set; }
        public Brush TirsdagFarve { get; set; }
        public Brush OnsdagFarve { get; set; }
        public Brush TorsdagFarve { get; set; }
        public Brush FredagFarve { get; set; }
        public Brush LoerdagFarve { get; set; }
        public Brush SoendagFarve { get; set; }

        public int Ugenummer { get; set; }

        public string Mandag { get; set; }
        public string Tirsdag { get; set; }
        public string Onsdag { get; set; }
        public string Torsdag { get; set; }
        public string Fredag { get; set; }
        public string Loerdag { get; set; }
        public string Soendag { get; set; }
        public ObservableCollection<Ugedage> UgedageCollection { get; set; }

        public ICommand AlleVagterCommand { get; set; }
        public ICommand FrieVagterCommand { get; set; }
        public ICommand MineVagterCommand { get; set; }

        public MainViewModel()
        {
            NuvaerendeUgedag(new SolidColorBrush(Color.FromArgb(100, 255, 255, 255)), new SolidColorBrush(Color.FromArgb(100, 162, 218, 255)));

            FindUgenummer("da-DK");
            Mandag = FoersteDagPaaUge(Ugenummer).ToString("dd/MM-yyyy");
            Tirsdag = FoersteDagPaaUge(Ugenummer).AddDays(1).ToString("dd/MM-yyyy");
            Onsdag = FoersteDagPaaUge(Ugenummer).AddDays(2).ToString("dd/MM-yyyy");
            Torsdag = FoersteDagPaaUge(Ugenummer).AddDays(3).ToString("dd/MM-yyyy");
            Fredag = FoersteDagPaaUge(Ugenummer).AddDays(4).ToString("dd/MM-yyyy");
            Loerdag = FoersteDagPaaUge(Ugenummer).AddDays(5).ToString("dd/MM-yyyy");
            Soendag = FoersteDagPaaUge(Ugenummer).AddDays(6).ToString("dd/MM-yyyy");

            AlleVagterCommand = new RelayCommand(AlleVagter);
            FrieVagterCommand = new RelayCommand(FrieVagter);
            MineVagterCommand = new RelayCommand(MineVagter);

            #region Random genereret data
            UgedageCollection = new ObservableCollection<Ugedage>()
            {
                new Ugedage {Ugedag = "Mandag", AnsatteListe = new List<Ansatte>()},
                new Ugedage {Ugedag = "Tirsdag", AnsatteListe = new List<Ansatte>()},
                new Ugedage {Ugedag = "Onsdag", AnsatteListe = new List<Ansatte>()},
                new Ugedage {Ugedag = "Torsdag", AnsatteListe = new List<Ansatte>()},
                new Ugedage {Ugedag = "Fredag", AnsatteListe = new List<Ansatte>()},
                new Ugedage {Ugedag = "Lørdag", AnsatteListe = new List<Ansatte>()},
                new Ugedage {Ugedag = "Søndag", AnsatteListe = new List<Ansatte>()},
            };
            Random r = new Random();
            for (int i = 0; i < UgedageCollection.Count/2; i++)
            {
                UgedageCollection[r.Next(0, UgedageCollection.Count)].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Daniel Winther",
                    Tidspunkt = "16:00 - 22:30"
                });
                UgedageCollection[r.Next(0, UgedageCollection.Count)].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Benjamin Jensen",
                    Tidspunkt = "7:00 - 16:50"
                });
                UgedageCollection[r.Next(0, UgedageCollection.Count)].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Jari Larsen",
                    Tidspunkt = "8:30 - 17:30"
                });
                UgedageCollection[r.Next(0, UgedageCollection.Count)].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Jacob Balling",
                    Tidspunkt = "6:00 - 14:20"
                });
                UgedageCollection[r.Next(0, UgedageCollection.Count)].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Peter Hansen",
                    Tidspunkt = "10:00 - 18:00"
                });
                UgedageCollection[r.Next(0, UgedageCollection.Count)].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Christian Johansen",
                    Tidspunkt = "11:00 - 15:30"
                });
                UgedageCollection[r.Next(0, UgedageCollection.Count)].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Ubemandet",
                    Tidspunkt = "8:00 - 21:40"
                });
            }
            #endregion

        }
        public void NuvaerendeUgedag(SolidColorBrush brush, SolidColorBrush brushOriginal)
        {
            if (MandagFarve == null || TirsdagFarve == null || OnsdagFarve == null || TorsdagFarve == null || FredagFarve == null || LoerdagFarve == null || SoendagFarve == null)
            {
                MandagFarve = brushOriginal;
                TirsdagFarve = brushOriginal;
                OnsdagFarve = brushOriginal;
                TorsdagFarve = brushOriginal;
                FredagFarve = brushOriginal;
                LoerdagFarve = brushOriginal;
                SoendagFarve = brushOriginal;
            }
            switch (DateTime.Now.ToString("dddd"))
            {
                case "Monday":
                    MandagFarve = brush;
                    break;

                case "Tuesday":
                    TirsdagFarve = brush;
                    break;

                case "Wednesday":
                    OnsdagFarve = brush;
                    break;

                case "Thursday":
                    TorsdagFarve = brush;
                    break;

                case "Friday":
                    FredagFarve = brush;
                    break;

                case "Saturday":
                    LoerdagFarve = brush;
                    break;

                case "Sunday":
                    SoendagFarve = brush;
                    break;
            }
        }
        public void FindUgenummer(string kulturInfo)
        {
            var kultur = CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(kulturInfo);
            Ugenummer = kultur.Calendar.GetWeekOfYear(DateTime.Today, DateTimeFormatInfo.GetInstance(kultur).CalendarWeekRule, DateTimeFormatInfo.GetInstance(kultur).FirstDayOfWeek);
        }
        public DateTime FoersteDagPaaUge(int weekOfYear)
        {
            DateTime jan1 = new DateTime(DateTime.Today.Year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }
        public void AlleVagter()
        {
            UgedageCollection.Clear();
        }
        public void FrieVagter()
        {
            UgedageCollection.Clear();
        }

        public void MineVagter()
        {
            UgedageCollection.Clear();
        }
    }

    #region Forsøgsklasser
    internal class Ugedage
    {
        public string Ugedag { get; set; }
        public List<Ansatte> AnsatteListe { get; set; }
    }

    internal class Ansatte
    {
        public string Navn { get; set; }
        public string Tidspunkt { get; set; }
    }
    #endregion
}