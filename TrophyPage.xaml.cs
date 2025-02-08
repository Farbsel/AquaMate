using System.Collections.ObjectModel;

namespace AquaMate;

public partial class TrophyPage : ContentPage
{
    public TrophyPage()
    {
        InitializeComponent();
        BindingContext = new TrophyPageViewModel(); // Setze das ViewModel als BindingContext
        int streak = Preferences.Get("streak", 0);
        lblStreak.Text = streak.ToString();

        if(lblStreak.Text =="")
        {
            lblStreak.Text = "0";
        }
    }

    // ViewModel als lokale Unterklasse
    public class TrophyPageViewModel
    {
        public ObservableCollection<LeaderboardItem> Leaderboard { get; set; }

        public TrophyPageViewModel()
        {
            Leaderboard = new()
                {
                    new LeaderboardItem { Level = 1, Name = "Max", Points = 500 },
                    new LeaderboardItem { Level = 2, Name = "Anna", Points = 450 },
                    new LeaderboardItem { Level = 3, Name = "Lukas", Points = 420 },
                };
        }
    }

    public class LeaderboardItem
    {
        public int Level { get; set; }
        public required string Name { get; set; }
        public int Points { get; set; }
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
}