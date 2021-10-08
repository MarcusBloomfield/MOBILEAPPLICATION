using MOBILEAPPLICATION.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using MOBILEAPPLICATION.Model;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MOBILEAPPLICATION.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        LoadApplicationSettings loadApplicationSettings = new LoadApplicationSettings();
        public Login()
        {
            InitializeComponent();
            loadApplicationSettings.Load();
        }
        private void CreateAccountClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CreateAccount());
        }
        private async void LoginClicked(object sender, EventArgs e)
        {
            Console.WriteLine("a");
            // checks for valid entered pass word and name, Runs login function from database connector, grabs users ntoes, creates categories and pushes category list
            if (UserName.Text?.Length > 0 && Password.Text?.Length > 0)
            {
                Console.WriteLine("b" + UserName.Text + Password.Text);
                if (Connectivity.NetworkAccess != NetworkAccess.None)
                {
                    Console.WriteLine("c");
                    string notes = await DatabaseConnectionManager.Login(UserName.Text, Password.Text);

                    if (notes.Length > 0)
                    {
                        Console.WriteLine("d");
                        ObservableCollection<Note> deserialisedNotes = JsonConvert.DeserializeObject<ObservableCollection<Note>>(notes);
                        CategoryList categoryList = new CategoryList(deserialisedNotes);
                        await Navigation.PushModalAsync(categoryList);
                    }
                    else
                    {
                        await DisplayAlert("No Account", "Please Create an Account", "Close");
                    }
                }
                else
                {
                    await DisplayAlert("No internet", "Please Connect to the internet", "Close");
                }
            }
        }
    }
}