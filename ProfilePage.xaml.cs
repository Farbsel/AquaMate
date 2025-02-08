namespace AquaMate;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
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

    private async void OnEditProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditProfilePage());
    }


}