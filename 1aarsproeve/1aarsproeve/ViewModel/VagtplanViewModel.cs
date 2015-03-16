using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using _1aarsproeve.Common;

namespace _1aarsproeve.ViewModel
{
    class VagtplanViewModel
    {
        public ApplicationDataContainer Setting { get; set; }
        public string Brugernavn { get; set; }

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

        public VagtplanViewModel()
        {
            Setting = ApplicationData.Current.LocalSettings;
            Setting.Values["Brugernavn"] = "Daniel Winther";

            Brugernavn = (string) Setting.Values["Brugernavn"];

            NuvaerendeUgedag(new SolidColorBrush(Color.FromArgb(100, 255, 255, 255)), new SolidColorBrush(Color.FromArgb(100, 162, 218, 255)));

            FindUgenummer("da-DK");
            Mandag = FoersteDagPaaUge(Ugenummer).ToString("D", new CultureInfo("da-DK"));
            Tirsdag = FoersteDagPaaUge(Ugenummer).AddDays(1).ToString("D", new CultureInfo("da-DK"));
            Onsdag = FoersteDagPaaUge(Ugenummer).AddDays(2).ToString("D", new CultureInfo("da-DK"));
            Torsdag = FoersteDagPaaUge(Ugenummer).AddDays(3).ToString("D", new CultureInfo("da-DK"));
            Fredag = FoersteDagPaaUge(Ugenummer).AddDays(4).ToString("D", new CultureInfo("da-DK"));
            Loerdag = FoersteDagPaaUge(Ugenummer).AddDays(5).ToString("D", new CultureInfo("da-DK"));
            Soendag = FoersteDagPaaUge(Ugenummer).AddDays(6).ToString("D", new CultureInfo("da-DK"));

            InitialiserUgedage();
            InitialiserAnsatte();

            AlleVagterCommand = new RelayCommand(AlleVagter);
            FrieVagterCommand = new RelayCommand(FrieVagter);
            MineVagterCommand = new RelayCommand(MineVagter);
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
        public DateTime FoersteDagPaaUge(int ugePaaAaret)
        {
            DateTime jan1 = new DateTime(DateTime.Today.Year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime foersteTorsdag = jan1.AddDays(daysOffset);
            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(foersteTorsdag, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var ugenummer = ugePaaAaret;
            if (firstWeek <= 1)
            {
                ugenummer -= 1;
            }
            var resultat = foersteTorsdag.AddDays(ugenummer * 7);
            return resultat.AddDays(-3);
        }

        #region InitialiserUgedage
        public void InitialiserUgedage()
        {
            UgedageCollection = new ObservableCollection<Ugedage>()
            {
                new Ugedage {Ugedag = "Mandag", AnsatteListe = new ObservableCollection<Ansatte>()},
                new Ugedage {Ugedag = "Tirsdag", AnsatteListe = new ObservableCollection<Ansatte>()},
                new Ugedage {Ugedag = "Onsdag", AnsatteListe = new ObservableCollection<Ansatte>()},
                new Ugedage {Ugedag = "Torsdag", AnsatteListe = new ObservableCollection<Ansatte>()},
                new Ugedage {Ugedag = "Fredag", AnsatteListe = new ObservableCollection<Ansatte>()},
                new Ugedage {Ugedag = "Lørdag", AnsatteListe = new ObservableCollection<Ansatte>()},
                new Ugedage {Ugedag = "Søndag", AnsatteListe = new ObservableCollection<Ansatte>()},
            };
        }
        #endregion

        #region InitialiserAnsatte
        public void InitialiserAnsatte()
        {
            for (int i = 0; i < UgedageCollection.Count; i++)
            {
                UgedageCollection[i].AnsatteListe.Clear();

                UgedageCollection[i].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Daniel Winther",
                    Tidspunkt = "16:30 - 20:30"
                });
                UgedageCollection[i].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Benjamin Jensen",
                    Tidspunkt = "07:00 - 16:50"
                });
                UgedageCollection[i].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Jari Larsen",
                    Tidspunkt = "16:00 - 19:50"
                });
                UgedageCollection[i].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Jacob Balling",
                    Tidspunkt = "06:00 - 14:20"
                });
                UgedageCollection[i].AnsatteListe.Add(new Ansatte
                {
                    Navn = "Ubemandet",
                    Tidspunkt = "08:00 - 12:50"
                });
            }
            UgedageCollection[0].AnsatteListe.Add(new Ansatte
            {
                Navn = "Daniel Winther",
                Tidspunkt = "16:00 - 19:50"
            });
            UgedageCollection[4].AnsatteListe.Add(new Ansatte
            {
                Navn = "Ubemandet",
                Tidspunkt = "15:00 - 18:10"
            });
            for (int i = 0; i < UgedageCollection.Count; i++)
            {
                var query =
                    from u in UgedageCollection[i].AnsatteListe.ToList()
                    orderby u.Tidspunkt, u.Navn ascending
                    select u;
                UgedageCollection[i].AnsatteListe.Clear();
                foreach (var ansatte in query)
                {
                    UgedageCollection[i].AnsatteListe.Add(ansatte);
                }
            }
        }
        #endregion

        public void AlleVagter()
        {
            InitialiserAnsatte();
        }
        public void FrieVagter()
        {
            InitialiserAnsatte();
            for (int i = 0; i < UgedageCollection.Count; i++)
            {
                var query =
                    from u in UgedageCollection[i].AnsatteListe.ToList()
                    where u.Navn == "Ubemandet"
                    orderby u.Tidspunkt, u.Navn ascending 
                    select u;
                UgedageCollection[i].AnsatteListe.Clear();
                foreach (var ansatte in query)
                {
                    UgedageCollection[i].AnsatteListe.Add(ansatte);
                }
            }
        }
        public void MineVagter()
        {
            InitialiserAnsatte();
            for (int i = 0; i < UgedageCollection.Count; i++)
            {
                var query =
                    from u in UgedageCollection[i].AnsatteListe.ToList()
                    where u.Navn == Brugernavn
                    orderby u.Tidspunkt, u.Navn ascending 
                    select u;
                UgedageCollection[i].AnsatteListe.Clear();

                foreach (var ansatte in query)
                {
                    UgedageCollection[i].AnsatteListe.Add(ansatte);
                }
            }
        }
    }
    #region Forsøgsklasser
    internal class Ugedage
    {
        public string Ugedag { get; set; }
        public ObservableCollection<Ansatte> AnsatteListe { get; set; }
    }

    internal class Ansatte
    {
        public string Navn { get; set; }
        public string Tidspunkt { get; set; }
    }
    #endregion
}