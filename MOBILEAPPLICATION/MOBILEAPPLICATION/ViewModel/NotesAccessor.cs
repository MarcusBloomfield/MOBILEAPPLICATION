using MOBILEAPPLICATION.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MOBILEAPPLICATION.ViewModel
{
    public static class NotesAccessor
    {
        // this class handles all of the accessing of the notfunctions class through static methods
        // this allows us to globally have one or a set of notesFunctions that we can access from anywhere we need
        static NotesFunctions notesFunctions = new NotesFunctions();
        internal static NotesFunctions MainPageViewModel { get => notesFunctions; set => notesFunctions = value; }

        public static ObservableCollection<Category> Categories
        {
            get => MainPageViewModel.Categories;
            set => MainPageViewModel.Categories = value;
        }
        public static ObservableCollection<Note> Notes
        {
            get => MainPageViewModel.Notes;
            set => MainPageViewModel.Notes = value;
        }

        public static void CreateCategories()
        {
            MainPageViewModel.CreateCategories();
        }
        public static ObservableCollection<Note> SetNotesByCategory(string category)
        {
            return MainPageViewModel.DisplayItemsByType(category, notesFunctions.Notes);
        }
        public static void SaveNote(Note note)
        {
            notesFunctions.SaveNote(note);
        }
        public static void DeleteNote(Note note)
        {
            notesFunctions.DeleteNote(note);
        }
        public static void CreateDefaultNote()
        {
            notesFunctions.CreateDefaultNote();
        }
        public static void SortCategories(ListView listView)
        {
            notesFunctions.Sort(listView, notesFunctions.Categories);
        }
        public static void SearchCategories(ListView listView, string search)
        {
            notesFunctions.Search(listView, search, notesFunctions.Categories);
        }
        public static void SortNotes(ListView listView)
        {
            notesFunctions.Sort(listView, notesFunctions.NotesByCategory);
        }
        public static void SearchNotes(ListView listView, string search)
        {
            notesFunctions.Search(listView, search, notesFunctions.NotesByCategory);
        }
        public static void UpdateNotesInDatabaseAndClearFromMemory()
        {
            notesFunctions.UpdateNotes();
            notesFunctions.Notes.Clear();
            notesFunctions.Categories.Clear();
            notesFunctions.NotesByCategory.Clear();
        }
    }
}
