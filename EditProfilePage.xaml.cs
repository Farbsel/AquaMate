using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace AquaMate;

public partial class EditProfilePage : ContentPage
{
    bool privacySetter = true;

    public EditProfilePage()
    {
        InitializeComponent();
        Preferences.Set("name", "Max Mustermann");
        Preferences.Set("passwort", "1234");
        Preferences.Set("email", "email@email.com");
        Preferences.Set("privacy", true);

        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        string name = Preferences.Get("name", "Standardwert");
        string passwort = Preferences.Get("passwort", "Standardwert");
        string email = Preferences.Get("email", "Standardwert");
        bool privacy = Preferences.Get("privacy", false);


        tbxEmail.Text = email;
        tbxName.Text = name;
        tbxPW.Text = passwort;


        if (privacy == true)
        {
            btnPublic.BackgroundColor = Color.FromArgb("#FFD700");
            btnPrivat.BackgroundColor = Color.FromArgb("#FFFFFF");
        }
        else
        {
            btnPublic.BackgroundColor = Color.FromArgb("#FFFFFF");
            btnPrivat.BackgroundColor = Color.FromArgb("#FFD700");
        }

    }

    public void OnPrivacyClicked(object sender, EventArgs e)
    {
        if (sender is Button Button && Button.CommandParameter is string param)
        {
            int selectedPrivacy = int.Parse(param);
            if (selectedPrivacy == 1)
            {
                // Public
                btnPublic.BackgroundColor = Color.FromArgb("#FFD700");
                btnPrivat.BackgroundColor = Color.FromArgb("#FFFFFF");
                privacySetter = true;
            }
            else if (selectedPrivacy == 2)
            {
                // Privat
                btnPublic.BackgroundColor = Color.FromArgb("#FFFFFF");
                btnPrivat.BackgroundColor = Color.FromArgb("#FFD700");
                privacySetter = false;
            }
        }
    }

    public void OnSpeichernClicked (object sender, EventArgs e)
    {
        Preferences.Set("name", tbxName.Text);
        Preferences.Set("passwort", tbxPW.Text);
        Preferences.Set("email", tbxEmail.Text);
        Preferences.Set("privacy", privacySetter);
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