namespace AquaMate
{
    public class GlobalVar
    {
        private static GlobalVar? _instance;

        // Stellt sicher, dass nur eine Instanz existiert
        public static GlobalVar Instance => _instance ??= new GlobalVar();

        // Username speichern und abrufen
        public string Username { 
            get => Preferences.Get("username", "Standardwert"); 
            set => Preferences.Set("username", value); 
        }
    
        public double Soll {

            get => Preferences.Get("soll", 0);
            set => Preferences.Set("soll", value);
        }
        public double Haben
        {
            get => Preferences.Get("haben", 0);
            set => Preferences.Set("haben", value);
        }

        public int Streak
        {
            get => Preferences.Get("streak", 0);
            set => Preferences.Set("streak", value);
        }

        public bool Privacy
        {
            get => Preferences.Get("privacy", false);
            set => Preferences.Set("privacy", value);
        }

        public string Email
        {
            get => Preferences.Get("email", "Standardwert");
            set => Preferences.Set("email", value);
        }

        public string Password
        {
            get => Preferences.Get("password", "Standardwert");
            set => Preferences.Set("password", value);
        }

    }
}
