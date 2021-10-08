using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MOBILEAPPLICATION.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void DarkMode(object sender, EventArgs e)
        {
            // sets the app theme on click
            if (Application.Current.UserAppTheme == OSAppTheme.Light || Application.Current.UserAppTheme == OSAppTheme.Unspecified)
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
                Application.Current.Properties["IsDarkMode"] = true;
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
                Application.Current.Properties["IsDarkMode"] = false;
            }
        }
    }
}