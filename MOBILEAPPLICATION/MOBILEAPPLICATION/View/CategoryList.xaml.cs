using MOBILEAPPLICATION.Model;
using MOBILEAPPLICATION.ViewModel;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MOBILEAPPLICATION.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryList : ContentPage
    {
        public CategoryList(ObservableCollection<Note> notes)
        {
            // sets notes to the users from the database after login
            NotesAccessor.Notes = notes;
            NotesAccessor.CreateCategories();
            BindingContext = NotesAccessor.MainPageViewModel;
            InitializeComponent();
        }
        public CategoryList()
        {
            // creates new notes and categories when the user has created a new account
            NotesAccessor.CreateDefaultNote();
            NotesAccessor.CreateCategories();
            BindingContext = NotesAccessor.MainPageViewModel;
            InitializeComponent();
        }
        private void NewNote(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NotesEditor());
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // gets category from category list, sorts the wanted notes into a new list by the selected category, then pushes notelist
            Category category = e.Item as Category;
            if (category == null && category.Name != null)
                return;

            NotesList noteList = new NotesList();
            NotesAccessor.SetNotesByCategory(category.Name);
            noteList.BindingContext = NotesAccessor.MainPageViewModel;

            Navigation.PushModalAsync(noteList);
        }

        private void Sort(object sender, EventArgs e)
        {
            NotesAccessor.SortCategories(listview);
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            NotesAccessor.CreateCategories();
            listview.IsRefreshing = false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NotesAccessor.CreateCategories();
        }

        private async void Settings(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SettingsPage());
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotesAccessor.SearchCategories(listview, e.NewTextValue);
        }
    }
}