using MOBILEAPPLICATION.Model;
using MOBILEAPPLICATION.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MOBILEAPPLICATION.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesEditor : ContentPage
    {
        public Note Note = new Note();
        public NotesEditor()
        {
            InitializeComponent();
            if (Note.Date == null)
            {
                Note.Date = DateTime.Now;
            }
        }
        private void Save(object sender, EventArgs e)
        {
            if (noteName.Text != null && noteBody.Text != null && noteCategory.Text != null)
            {
                // saves the new note that has data in it to the notes list in Notes functions
                Note note = new Note { Name = noteName.Text, Date = DateTime.Now, Category = noteCategory.Text, Content = noteBody.Text };
                NotesAccessor.SaveNote(note);
            }
        }
        private async void Delete(object sender, EventArgs e)
        {
            if (noteName.Text != null && noteBody.Text != null && noteCategory.Text != null)
            {
                bool Delete = await DisplayAlert("Delete", "Are you sure you want to delete this", "Yes", "No");
                if (Delete)
                {
                    // deletes the note from the noteslist in notes functions
                    Note note = new Note { Name = noteName.Text, Date = DateTime.Now, Category = noteCategory.Text, Content = noteBody.Text };
                    NotesAccessor.DeleteNote(note);
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}