using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.ComponentModel;
using MOBILEAPPLICATION.Model;
using System.Linq;

namespace MOBILEAPPLICATION.ViewModel
{
    class NotesFunctions : INotifyPropertyChanged
    {
        // this class uses the inotify property changed interface to allow us to update anything we have on an event
        ObservableCollection<Note> notes = new ObservableCollection<Note>();
        ObservableCollection<Category> categories = new ObservableCollection<Category>();
        ObservableCollection<Note> notesByCategory = new ObservableCollection<Note>();
        int categorySortIterator = 0;
        int noteSortIterator = 0;

        public ObservableCollection<Note> NotesByCategory
        {
            get => notesByCategory;
            set
            {
                if (notesByCategory == value)
                    return;
                notesByCategory = value;
                OnPropertyChanged(nameof(notesByCategory));
            }
        }
        public ObservableCollection<Category> Categories
        {
            get => categories;
            set
            {
                if (categories == value)
                    return;
                categories = value;
                OnPropertyChanged(nameof(categories));
            }
        }
        public ObservableCollection<Note> Notes
        {
            get => notes;
            set
            {
                if (notes == value)
                    return;
                notes = value;
                OnPropertyChanged(nameof(notes));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            Console.WriteLine("property changed" + name);
        }
        public void CreateCategories()
        {
            // dynamicly creates categories form the notes by accessing their notes.category property
            Categories.Clear();
            foreach (var note in Notes)
            {
                if (!Categories.Any(category => category.Name == note.Category))
                {
                    Category newCategory = new Category
                    {
                        Name = note.Category,
                        Date = note.Date
                    };
                    Categories.Add(newCategory);
                }
            }
        }
        public ObservableCollection<Note> DisplayItemsByType(string category, ObservableCollection<Note> notes)
        {
            // allows us to only display notes of a certain type of category from the note.category property
            //pushes the selected notes to the notes by category list that is bound to the notes list view
            if (category != null) NotesByCategory.Clear();
            foreach (var item in notes)
            {
                if (item.Category == category)
                {
                    NotesByCategory.Add(item);
                }
            }
            return NotesByCategory;
        }
        public async void SaveNote(Note note)
        {
            CollectNotesByNameThenRemoveFromList(note);
            Notes.Add(note);
            await DatabaseConnectionManager.UpdateNote(NotesAccessor.Notes);
        }
        public async void DeleteNote(Note note)
        {
            CollectNotesByNameThenRemoveFromList(note);
            RemoveDuplicateNotes(note);
            await DatabaseConnectionManager.UpdateNote(NotesAccessor.Notes);
        }
        void CollectNotesByNameThenRemoveFromList(Note note)
        {
            // collects identical notes and deletes them
            List<Note> notesToDelete = new List<Note>();
            foreach (Note item in Notes)
            {
                if (item.Name == note.Name && item.Category == note.Category)
                {
                    note.Content = note.Content + Environment.NewLine + Environment.NewLine + " Content From Old Note : " + item.Content;
                    notesToDelete.Add(item);
                }
            }
            foreach (Note item in notesToDelete)
            {
                Notes.Remove(item);
            }
        }
        void RemoveDuplicateNotes(Note note)
        {
            //collects duplicate notes and deletes them
            List<Note> notesToDelete = new List<Note>();
            foreach (Note item in Notes)
            {
                if (item.Name == note.Name && item.Category == note.Category && item.Content == note.Content)
                {
                    notesToDelete.Add(item);
                }
            }
            foreach (Note item in notesToDelete)
            {
                Notes.Remove(item);
            }
        }
        public void CreateDefaultNote()
        {
            // creates a default note for new accounts
            Note defaultNote = new Note
            {
                Name = "Defult Note",
                Category = "Default Note",
                Content = "THIS IS A DEFAULT NOTE"
            };
            SaveNote(defaultNote);
        }
        public void Sort(ListView listView, ObservableCollection<Category> ListToSearch)
        {
            // upon button press sorts the notes by name and date
            listView.IsRefreshing = true;
            if (categorySortIterator == 0)
            {
                ListToSearch = new ObservableCollection<Category>(ListToSearch.OrderByDescending(i => i.Name));
                categorySortIterator++;
            }
            else if (categorySortIterator == 1)
            {
                ListToSearch = new ObservableCollection<Category>(ListToSearch.OrderBy(i => i.Name)); 
                categorySortIterator++;
            }
            else if (categorySortIterator == 2)
            {
                ListToSearch = new ObservableCollection<Category>(ListToSearch.OrderBy(i => i.Date));
                categorySortIterator++;
            }
            else
            {
                ListToSearch = new ObservableCollection<Category>(ListToSearch.OrderByDescending(i => i.Date));
                categorySortIterator = 0;
            }
            listView.ItemsSource = ListToSearch;
            listView.IsRefreshing = false;
        }
        public void Search(ListView listView, string searchText, ObservableCollection<Category> ListToSearch)
        {
            // searchs for categories via the name
            if (!string.IsNullOrEmpty(searchText))
            {
                ListToSearch = new ObservableCollection<Category>(ListToSearch.Where(i => i.Name.Contains(searchText)));
            }
            else
            {
                CreateCategories();
            }
            listView.ItemsSource = ListToSearch;
            listView.IsRefreshing = false;
        }
        public void Sort(ListView listView, ObservableCollection<Note> ListToSearch)
        {
            // upon button press sorts the categories by name and date
            listView.IsRefreshing = true;
            if (noteSortIterator == 0)
            {
                ListToSearch = new ObservableCollection<Note>(ListToSearch.OrderByDescending(i => i.Name));
                noteSortIterator++;
            }
            else if (noteSortIterator == 1)
            {
                ListToSearch = new ObservableCollection<Note>(ListToSearch.OrderBy(i => i.Name));
                noteSortIterator++;
            }
            else if (noteSortIterator == 2)
            {
                ListToSearch = new ObservableCollection<Note>(ListToSearch.OrderBy(i => i.Date));
                noteSortIterator++;
            }
            else
            {
                ListToSearch = new ObservableCollection<Note>(ListToSearch.OrderByDescending(i => i.Date));
                noteSortIterator = 0;
            }
            listView.ItemsSource = ListToSearch;
            listView.IsRefreshing = false;
        }
        public void Search(ListView listView, string searchText, ObservableCollection<Note> ListToSearch)
        {
            // searchs for notes via the name
            if (!string.IsNullOrEmpty(searchText))
            {
                ListToSearch = new ObservableCollection<Note>(ListToSearch.Where(i => i.Name.Contains(searchText)));
            }
            else
            {
                CreateCategories();
            }
            listView.ItemsSource = ListToSearch;
            listView.IsRefreshing = false;
        }
        public async void UpdateNotes()
        {
           await DatabaseConnectionManager.UpdateNote(notes);
        }
    }
}
