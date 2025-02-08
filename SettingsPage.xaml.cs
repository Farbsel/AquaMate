namespace AquaMate;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
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


    private async void BtnShopClicked(object sender, EventArgs e)
    {
        await Launcher.OpenAsync("https://www.AquaMate.at");
    }

    // Event-Handler für den "Download" Button
    private void BtnDownloadClicked(object sender, EventArgs e)
    {
        DisplayAlert("Info", "Das Layout wurde geklickt!", "OK");
    }

    // Event-Handler für den "Sprache" Button
    private void BtnSpracheClicked(object sender, EventArgs e)
    {
        DisplayAlert("Info", "Das Layout wurde geklickt!", "OK");
    }

    // Event-Handler für den "Premium" Button
    private void BtnPremiumClicked(object sender, EventArgs e)
    {
        DisplayAlert("Info", "Das Layout wurde geklickt!", "OK");
    }

    // Event-Handler für den "Profil wechseln" Button
    private void BtnProfilBearbeitenClicked(object sender, EventArgs e)
    {
        //Profil bearbeiten seite
    }

    // Event-Handler für den "Log out" Button
    private async void BtnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Abmelden", "Bist du sicher, dass du dich abmelden möchtest?", "Ja", "Nein");

        if (confirm)
        {
            // Deine Logik für den Logout
            Console.WriteLine("Benutzer hat sich abgemeldet.");
            // Weitere Logik hier
        }
        else
        {
            Console.WriteLine("Abmeldung abgebrochen.");
        }
    }

    private async void BtnRechnerClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Rechner());
    }



}