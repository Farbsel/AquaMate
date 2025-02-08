namespace AquaMate;

public partial class Rechner : ContentPage
{

    //Variablen
    public double FGender = 15;
    public double FPal = 15;
    public double FAthlet = 15;

    public Rechner()
    {
        InitializeComponent();
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        if (sender is Entry entry)
        {
            if (!double.TryParse(entry.Text, out _))
            {
                DisplayAlert("Fehler", "Bitte geben Sie eine gültige Zahl ein.", "OK");
                entry.Text = "";  // Lösche den ungültigen Eingabewert
                
            }
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

    public void OnGenderClicked(object sender, EventArgs e)
    {

        if (sender is Button Button && Button.CommandParameter is string param)
        {
            int selectedGender = int.Parse(param);

            if (selectedGender == 1)
            {
                // Männlich
                BtnMan.BackgroundColor = Color.FromArgb("#FFD700");
                BtnWom.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnDiv.BackgroundColor = Color.FromArgb("#FFFFFF");
                FGender = 1;
            }
            else if (selectedGender == 2)
            {
                // Weiblich
                BtnMan.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnWom.BackgroundColor = Color.FromArgb("#FFD700");
                BtnDiv.BackgroundColor = Color.FromArgb("#FFFFFF");
                FGender = 0;
            }
            else if (selectedGender == 3)
            {
                // Divers
                BtnMan.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnWom.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnDiv.BackgroundColor = Color.FromArgb("#FFD700");
                FGender = 0.5;

            }
            else
            {
                // Keine Auswahl                
                BtnMan.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnWom.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnDiv.BackgroundColor = Color.FromArgb("#FFFFFF");
                FGender = 1;

            }
        }


    }

    private void OnAthClicked(object sender, EventArgs e)
    {
        if (sender is Button Button && Button.CommandParameter is string param)
        {
            int selectedAth = int.Parse(param);
            if (selectedAth == 1)
            {
                // Athlet
                BtnAthJa.BackgroundColor = Color.FromArgb("#FFD700");
                BtnAthNein.BackgroundColor = Color.FromArgb("#FFFFFF");
                FAthlet = 1;
            }
            else if (selectedAth == 2)
            {
                // kein Athlet  
                BtnAthJa.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnAthNein.BackgroundColor = Color.FromArgb("#FFD700");
                FAthlet = 0;
            }
            else
            {
                // Nichts Ausgewaehlt  
                BtnAthJa.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnAthNein.BackgroundColor = Color.FromArgb("#FFFFFF");
                FAthlet = 15;
            }
        }
    }

    private void OnPalClicked(object sender, EventArgs e)
    {
        if (sender is Button Button && Button.CommandParameter is string param)
        {
            int selectedPAL = int.Parse(param);

            if (selectedPAL == 1)
            {
                // Kaum Aktiv
                BtnPALK.BackgroundColor = Color.FromArgb("#FFD700");
                BtnPalE.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPalM.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPALV.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPALSV.BackgroundColor = Color.FromArgb("#FFFFFF");

                FPal = 1.2;
            }
            else if (selectedPAL == 2)
            {
                // Etwas Aktiv
                BtnPALK.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPalE.BackgroundColor = Color.FromArgb("#FFD700");
                BtnPalM.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPALV.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPALSV.BackgroundColor = Color.FromArgb("#FFFFFF");

                FPal = 1.5;
            }
            else if (selectedPAL == 3)
            {
                // Mittel Aktiv
                BtnPALK.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPalE.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPalM.BackgroundColor = Color.FromArgb("#FFD700");
                BtnPALV.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPALSV.BackgroundColor = Color.FromArgb("#FFFFFF");

                FPal = 1.7;
            }
            else if (selectedPAL == 4)
            {
                // Viel Aktiv
                BtnPALK.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPalE.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPalM.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPALV.BackgroundColor = Color.FromArgb("#FFD700");
                BtnPALSV.BackgroundColor = Color.FromArgb("#FFFFFF");

                FPal = 1.9;
            }
            else if (selectedPAL == 5)
            {
                // Sehr Viel Aktiv
                BtnPALK.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPalE.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPalM.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPALV.BackgroundColor = Color.FromArgb("#FFFFFF");
                BtnPALSV.BackgroundColor = Color.FromArgb("#FFD700");

                FPal = 2.2;
            }
        }
    }
    private void OnRechnenClicked(object sender, EventArgs e)
    {
        if (FGender == 15 || FAthlet == 15 || FPal == 15 || TbxAlter.Text == "" || TbxSchlaf.Text == "" || TbxTemp.Text == "" || TbxGewicht.Text == "")
        {
            DisplayAlert("Fehler", "Bitte füllen Sie alle Felder aus!", "OK");
        }
        else
        {
            if (Double.TryParse(TbxAlter.Text, out double FAlter) &&
                Double.TryParse(TbxSchlaf.Text, out double FSchlaf) &&
                Double.TryParse(TbxTemp.Text, out double FTemp) &&
                Double.TryParse(TbxGewicht.Text, out double FGewicht))
            {
                // Berechnung
                
                FPal = (FPal*(24 - FSchlaf) + FSchlaf * 0.95);

                double HDI = 0.948;
                double WT = (1076 * FPal) +
                                      (14.34 * FGewicht) +
                                      (374.9 * FGender) +
                                      (1070 * FAthlet) +
                                      (104.6 * HDI) -
                                      Math.Sqrt(0.3529 * FAlter) +
                                      (24.78 * FAlter) +
                                      Math.Sqrt(1.865 * FTemp) -
                                      (19.66 * FTemp) - 713.1;

                

                Preferences.Set("soll", WT);
                DisplayAlert("Geschafft", "Dein Wasserkonsum ist " + WT, "OK");


            }
            else
            {
                DisplayAlert("Fehler", "Bitte geben Sie nur Zahlen ein!", "OK");
            }
        }
    }

}