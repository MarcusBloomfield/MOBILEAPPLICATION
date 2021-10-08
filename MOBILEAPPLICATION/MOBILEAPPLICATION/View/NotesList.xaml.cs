using MOBILEAPPLICATION.Model;
using MOBILEAPPLICATION.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MOBILEAPPLICATION.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesList : ContentPage
    {
        public NoteViewModel noteViewModel;
        public NotesList()
        {
            InitializeComponent();
            noteViewModel = new NoteViewModel();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NotesEditor());
        }

        async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // imports the note data from the listview to the noteviemodel class
            Note oldNote = e.Item as Note;
            if (oldNote == null)
                return;
            noteViewModel.Note.Name = oldNote.Name;
            noteViewModel.Note.Date = oldNote.Date;
            noteViewModel.Note.Content = oldNote.Content;
            noteViewModel.Note.Category = oldNote.Category;
            noteViewModel.Note.NameAndDate = oldNote.NameAndDate;
            //pushes the note view model class to a new page
            NotesEditor noteEditor = new NotesEditor
            {
                BindingContext = noteViewModel
            };
            await Navigation.PushModalAsync(noteEditor);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NotesAccessor.SetNotesByCategory(noteViewModel.Note.Category);
        }
        private async void Settings(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SettingsPage());
        }
        private void Sort(object sender, EventArgs e)
        {
            NotesAccessor.SortNotes(listview);
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotesAccessor.SearchNotes(listview, e.NewTextValue);
        }
    }
}