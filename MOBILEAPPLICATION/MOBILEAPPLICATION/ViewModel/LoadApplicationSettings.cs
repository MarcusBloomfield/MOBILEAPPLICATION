using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MOBILEAPPLICATION.ViewModel
{
    class LoadApplicationSettings
    {
        public void Load()
        {
            LoadAppTheme();
        }
        void LoadAppTheme()
        {
            // grabs the saved data from properties list and determines if the setting is active or not 
            string IsDarkMode = string.Empty;
            if (Application.Current.Properties.ContainsKey("IsDarkMode"))
            {
                IsDarkMode = Application.Current.Properties["IsDarkMode"].ToString();
            }
            bool DarkMode = Boolean.TryParse(IsDarkMode, out DarkMode);
            if (DarkMode)
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
            }
        }
    }
}
