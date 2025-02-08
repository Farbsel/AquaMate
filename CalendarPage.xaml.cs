    using Syncfusion.Maui.Calendar;
    using System.IO;
    using System.Runtime.InteropServices;


    namespace AquaMate;

    public partial class CalendarPage : ContentPage
    {
        //Db aufrufen
        private readonly LocalDbService _dbService = LocalDbService.Instance;

        // Liste der Streak-Daten (Markierte Tage)
        public CalendarPage()
        {
            InitializeComponent();

            calendar.SelectionChanged -= Calendar_SelectionChanged;
            calendar.SelectionChanged += Calendar_SelectionChanged;


        BindingContext = this;  // ANS ENDE VERSCHIEBEN

    }

    private void Calendar_SelectionChanged(object? sender, CalendarSelectionChangedEventArgs e)
        {
            calendar.SelectedDate = null;
        }

        private void CalendarSelectionChanged (object sender, CalendarSelectionChangedEventArgs e)
        {
            calendar.SelectedDate = null;
        }

            public async Task InitializeCalendarAsync()
        {
            try
            {

                List<Streakdays> streakDates = await LocalDbService.Instance.GetStreakdates();
                List<Streakdays> erreicht = await LocalDbService.Instance.GetDatesErreicht();

               


                calendar.MonthView.SpecialDayPredicate = (date) =>
                {
                    if (streakDates == null || streakDates.Count == 0)
                    {
                        return null;
                    }

                    var streak = streakDates.FirstOrDefault(s => s.Dates.Date == date.Date);
                    if (streak != null)
                    {
                        if (streak.Erreicht == true)
                        {
                            return new CalendarIconDetails
                            {
                                Fill = Color.FromArgb("79B14C") // grün für erreichte Tage
                                
                            };
                        }
                        else
                        {
                            return new CalendarIconDetails
                            {
                                Fill = Color.FromArgb("D85858") // rot für nicht erreichte Tage
                            };
                        }
                    }


                    

                    return null;
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing CalendarPage: {ex.Message}");
            }
        }

    

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(100);

        

            try
            {
                await Task.Delay(100); //kurz warten - Sichergehen das alles geladen ist

                await InitializeCalendarAsync();

                int streak = Preferences.Get("streak", 0);
                lblStreak.Text = streak.ToString();

                if (lblStreak.Text == "")
                {
                    lblStreak.Text = "0";
                }

                if (streak > 200)
                {
                    lblStreak.Text = streak.ToString();
                    ImgWasser.Source = "wassertropfen6.png";
                }
                else if (streak > 100)
                {
                    lblStreak.Text = streak.ToString();
                    ImgWasser.Source = "wassertropfen5.png";
                }
                else if (streak > 60)
                {
                    lblStreak.Text = streak.ToString();
                    ImgWasser.Source = "wassertropfen4.png";
                }
                else if (streak > 30)
                {
                    lblStreak.Text = streak.ToString();
                    ImgWasser.Source = "wassertropfen3.png";
                }
                else if (streak > 10)
                {
                    lblStreak.Text = streak.ToString();
                    ImgWasser.Source = "wassertropfen2.png";
                }
                else if (streak >= 0)
                {
                    lblStreak.Text = streak.ToString();
                    ImgWasser.Source = "wassertropfen1.png";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnAppearing: {ex.Message}");
            }
            }
        

        // Navigation zu anderen Seiten
        private async void OnHomePageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void OnTrophyPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TrophyPage());
        }

        private async void OnCalendarPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalendarPage());
        }

        private async void OnProfilePageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        private async void OnSettingsPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
