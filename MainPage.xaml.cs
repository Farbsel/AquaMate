using System.ComponentModel.Design;
using System.Reflection;
using Microsoft.Maui.ApplicationModel;


#if ANDROID
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Android.Content.PM;
using Android.OS;
using Android;

#endif

//test
namespace AquaMate
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService = LocalDbService.Instance;



        private Timer resetTimer = null!;
        private Timer reminderTimer = null!;

        public MainPage()
        {

            InitializeComponent();
            UpdateHabenLabel(); // Initialen Wert im Label anzeigen
            UpdateSollLabel();

            // Hole den aktuellen Wert von Soll
            double soll = Preferences.Get("soll", 0.0);
            if (soll == 0)
            {
                SollButton.IsVisible = true;
                lbl_prozent.IsVisible = false;
            }

            SetDailyResetTimer();
            SetReminderTimer();

            // Android-specific permission request
#if ANDROID
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu) // Android 13
            {
                var activity = Platform.CurrentActivity;
                if (activity != null)
                {
                    if (ContextCompat.CheckSelfPermission(activity, Manifest.Permission.PostNotifications) == Permission.Granted)
                    {
                        return;
                    }
                    ActivityCompat.RequestPermissions(activity, new string[] { Manifest.Permission.PostNotifications }, 1);
                }
            }
#endif


        }



        private void SetDailyResetTimer()
        {
            // Berechne die Zeit bis Mitternacht
            var currentTime = DateTime.Now;
            var nextMidnight = currentTime.Date.AddDays(1); // Setze auf den nächsten Tag um Mitternacht
            //var timeUntilMidnight = nextMidnight - currentTime;
            var timeUntilMidnight = TimeSpan.FromSeconds(10); // Testweise nur 10 Sekunden warten


            // Erstelle den Timer, der die Methode zum Zurücksetzen nach der berechneten Zeit aufruft
            resetTimer = new Timer(ResetHaben, null, (int)timeUntilMidnight.TotalMilliseconds, (int)TimeSpan.FromDays(1).TotalMilliseconds); // Wiederhole alle 24 Stunden
        }

        private void SetReminderTimer()
        {
            var now = DateTime.Now;
            var nextReminder = now.Date.AddHours(15).AddMinutes(46); // Setze auf 13:00 Uhr des aktuellen Tages

            // Falls die Zeit bereits vorbei ist, setze den Reminder auf den nächsten Tag
            if (now > nextReminder)
            {
                nextReminder = nextReminder.AddDays(1);
            }

            // Berechne die Zeitspanne bis zum nächsten Reminder
            var timeUntilReminder = nextReminder - now;

            // Erstelle den Timer, der den Reminder zur richtigen Zeit auslöst
            reminderTimer = new Timer(SendDrinkReminder, null, timeUntilReminder, TimeSpan.FromDays(1));
        }


        private void SendDrinkReminder(object? state)
        {
            try
            {

                // Hole den aktuellen Wert von Haben und Soll
                double haben = Preferences.Get("haben", 0.0);
                double soll = Preferences.Get("soll", 0.0);
                // Prüfen, ob Haben kleiner als Soll ist
                if (soll == haben / 2)
                {
#if ANDROID
                    var context = Android.App.Application.Context;

                    var builder = new NotificationCompat.Builder(context, "water_reminder")
                        .SetSmallIcon(Resource.Drawable.person) // Stelle sicher, dass du ein passendes Icon hast
                        .SetContentTitle("Trinken nicht vergessen!")
                        .SetContentText("Du hast heute noch nicht genug getrunken. Trink doch noch etwas Wasser!")
                        .SetPriority(NotificationCompat.PriorityHigh)
                        .SetAutoCancel(true);

                    var notificationManager = NotificationManagerCompat.From(context);
                    notificationManager.Notify(100, builder.Build());
#endif
                }
            }
            catch (TargetInvocationException ex)
            {
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
            }
        }

        private async void ResetHaben(object? state)
        {

            // Hole den aktuellen Wert von Haben und Soll
            double haben = Preferences.Get("haben", 0.0);
            double soll = Preferences.Get("soll", 0.0);




            // Prüfen, ob Haben größer als Soll ist
            if (haben > soll)
            {
                // Datum des Tages
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                int streak = Preferences.Get("streak", 0);
                streak += 1;
                Preferences.Set("streak", streak);
                // Speichern des Streak-Datums

                await _dbService.Create(new Streakdays
                {
                    Consumption = haben,
                    Dates = DateTime.Now,
                    Erreicht = true

                });


            }
            else
            {
                Preferences.Set("streak", 0);

                await _dbService.Create(new Streakdays
                {
                    Consumption = haben,
                    Dates = DateTime.Now,
                    Erreicht = false

                });
            }

            // Setze den Wert von Haben auf 0
            Preferences.Set("haben", 0.0);

            // UI-Update auf dem Hauptthread durchführen
            Dispatcher.Dispatch(() =>
            {
                UpdateHabenLabel();
            });
        }

       

        private void OnCounterClicked(object sender, EventArgs e)
        {
            // Hole den aktuellen Wert von Haben und Soll
            double haben = Preferences.Get("haben", 0.0);
            double soll = Preferences.Get("soll", 0.0);


            haben += 250; // 250 ml hinzufügen

            // Setze den neuen Wert von Haben
            Preferences.Set("haben", haben);

            UpdateHabenLabel(); // Aktualisierten Wert im Label anzeigen

            // Optional: Screenreader-Ankündigung
            SemanticScreenReader.Announce(lbl_haben.Text);



        }

        private void UpdateHabenLabel()
        {
            // Hole den aktuellen Wert von Haben und Soll
            double haben = Preferences.Get("haben", 0.0);
            double soll = Preferences.Get("soll", 0.0);


            // Wert in Liter umrechnen und im Label anzeigen
            lbl_haben.Text = (haben / 1000).ToString("0.0") + " L";
            lbl_prozent.Text = Math.Round((haben / soll) * 100).ToString() + "%";
            progressBar.Progress = (haben / soll) * 100;
        }

        private void UpdateSollLabel()
        {
            // Hole den aktuellen Wert von Soll
            double soll = Preferences.Get("soll", 0.0);


            // Wert in Liter umrechnen und im Label anzeigen
            lbl_soll.Text = (soll / 1000).ToString("0.0") + " L";
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

        private async void OnDetailPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetailPage());


        }
    }
}
