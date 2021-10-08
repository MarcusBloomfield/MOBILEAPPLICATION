using MOBILEAPPLICATION.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MOBILEAPPLICATION.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccount : ContentPage
    {
        public CreateAccount()
        {
            InitializeComponent();
        }
        private async void ConfirmClicked(object sender, EventArgs e)
        {
            if (userName.Text?.Length > 0 && passWord.Text?.Length > 0)
            {
                // checks if can create account
                bool CanCreateAccount = await DatabaseConnectionManager.CreateAccount(userName.Text, passWord.Text);
                if (CanCreateAccount)
                {
                    // removes notes from memory
                    NotesAccessor.UpdateNotesInDatabaseAndClearFromMemory();
                    await Navigation.PushModalAsync(new CategoryList());
                }
                else
                {
                    await DisplayAlert("Account Already Exists", "Please Choose another username", "Close");
                }
            }
        }
    }
}