using System.Collections.ObjectModel;

namespace AquaMate
{
    public partial class DetailPage : ContentPage
    {
        private readonly LocalDbService _dbService = LocalDbService.Instance;
        public ObservableCollection<Streakdays> StreakDataCollection { get; set; }


        public DetailPage()
        {
            InitializeComponent();



            StreakDataCollection = new ObservableCollection<Streakdays>();
            BindingContext = this;
            LoadData();

            double soll = Preferences.Get("soll", 0.0);
            double roundensoll = Math.Round(soll / 1000, 1);
            string soll2 = roundensoll.ToString();
            lblEmpfehlung.Text = "Du solltest jeden Tag mindestens " + soll2 + " Liter trinken";

        }

        private async void LoadData()
        {


            try
            {

                

                var last7Days = await _dbService.GetLast7Days();


                // Doppelte Einträge verhindern
                StreakDataCollection.Clear();

                foreach (var data in last7Days)
                {
                    Console.WriteLine($"Datum: {data.Dates}, Verbrauch: {data.Consumption}");
                    StreakDataCollection.Add(new Streakdays{
                        Dates = data.Dates,
                        Consumption = data.Consumption
                    });
                }


                var streakDates = last7Days.Select(x => new
                {
                    Streakdays = x.Dates.Date.ToString("dd.MM.yyyy"),
                    Consumption = x.Consumption
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        // Navigation zur Home-Seite
        private async void OnHomePageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        // Navigation zur Trophy-Seite
        private async void OnTrophyPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TrophyPage());
        }

        // Navigation zur Calendar-Seite
        private async void OnCalendarPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage());
        }

        // Navigation zur Profile-Seite
        private async void OnProfilePageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        // Navigation zur Settings-Seite
        private async void OnSettingsPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void OnRechnerClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Rechner());

        }

    }
}