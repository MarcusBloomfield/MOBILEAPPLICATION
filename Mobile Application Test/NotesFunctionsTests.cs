using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOBILEAPPLICATION.Model;
using MOBILEAPPLICATION.ViewModel;
using Xamarin.Forms;
namespace Mobile_Application_Test
{
    [TestClass]
    public class NotesFunctionsTests
    {
        [TestMethod]
        public void CreateCategoriesTestForIncorrectlyCreatedCategories()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = CreateRandomNotes(33);
            noteFunctions.CreateCategories();

            foreach (var item in noteFunctions.Categories)
            {
                if (!noteFunctions.Notes.Any(category => category.Category == item.Name))
                {
                    Assert.IsTrue(false);
                }
            }
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void CreateCategoriesTestForNullNotes()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = null;
            noteFunctions.CreateCategories();
        }
        [TestMethod]
        public void CreateCategoriesTestForEmptyNotes()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = new ObservableCollection<Note>();
            noteFunctions.CreateCategories();
        }
        [TestMethod]
        public void DisplayItemsByTypeTestCheckIfCreatingAllRequiredCategories()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = CreateNonRandomNotes(5);
            noteFunctions.CreateCategories();
            noteFunctions.DisplayItemsByType("a1", noteFunctions.Notes);

            if (!noteFunctions.NotesByCategory.Any(category => category.Category == "a1"))
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void DisplayItemsByTypeTestForNullCategoryString()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = CreateNonRandomNotes(5);
            noteFunctions.CreateCategories();
            noteFunctions.DisplayItemsByType(null, noteFunctions.Notes);
        }
        [TestMethod]
        public void DisplayItemsByTypeTestForNullNotes()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = CreateNonRandomNotes(5);
            noteFunctions.CreateCategories();
            noteFunctions.DisplayItemsByType("a", null);
        }
        [TestMethod]
        public void CollectNotesByNameThenRemoveFromListTestIfNoteHasBeenDeleted()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = CreateNonRandomNotes(5);
            noteFunctions.CreateCategories();
            noteFunctions.CollectNotesByNameThenRemoveFromList(new Note() { Name = "a", Category = "a1", Content = "a" });

            if (noteFunctions.Notes.Any(note => note.Name == "a"))
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void CollectNotesByNameThenRemoveFromListTestIfNoteOldNoteContentHasBeenAdded()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = CreateNonRandomNotes(5);
            noteFunctions.CreateCategories();
            var TestNote = new Note() { Name = "a", Category = "a1", Content = "a" };
            noteFunctions.CollectNotesByNameThenRemoveFromList(TestNote);

            if (!TestNote.Content.Contains("a2"))
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void CollectNotesByNameThenRemoveFromListTestIfNoteHasNullProperties()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = CreateNonRandomNotes(5);
            noteFunctions.CreateCategories();
            var TestNote = new Note() { Name = null, Category = null, Content = null };
            noteFunctions.CollectNotesByNameThenRemoveFromList(TestNote);
        }
        [TestMethod]
        public void CollectNotesByNameThenRemoveFromListTestIfNoteHasEmptyProperties()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = CreateNonRandomNotes(5);
            noteFunctions.CreateCategories();
            var TestNote = new Note() { Name = string.Empty, Category = string.Empty, Content = string.Empty };
            noteFunctions.CollectNotesByNameThenRemoveFromList(TestNote);
        }
        [TestMethod]
        public void CollectNotesByNameThenRemoveFromListTestIfNoteIsNull()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = CreateNonRandomNotes(5);
            noteFunctions.CreateCategories();
            Note TestNote = null;
            noteFunctions.CollectNotesByNameThenRemoveFromList(TestNote);
        }
        [TestMethod]
        public void RemoveDuplicateNotesTest()
        {
            NotesFunctions noteFunctions = new NotesFunctions();
            noteFunctions.Notes = CreateNonRandomNotes(5);
            noteFunctions.RemoveDuplicateNotes(new Note() { Name ="a", Category = "a1", Content="a2"});
            foreach (var item in noteFunctions.Notes)
            {
                if(item.Name == "a" && item.Category == "a1")
                {
                    Assert.IsTrue(false);
                }
            }
        }
        ObservableCollection<Note> CreateRandomNotes(int NoteAmount)
        {
            ObservableCollection<Note> notes = new ObservableCollection<Note>();
            for (int i = 0; i < NoteAmount; i++)
            {
                notes.Add(new Note() { Category = RandomString(11), Name = RandomString(11), Content = RandomString(11), Date = DateTime.Now });
            }
            return notes;
        }
        ObservableCollection<Note> CreateNonRandomNotes(int NumberOfNoteSets)
        {
            ObservableCollection<Note> notes = new ObservableCollection<Note>();
            for (int i = 0; i < NumberOfNoteSets; i++)
            {
                notes.Add(new Note() { Name = "a", Category = "a1", Content = "a2", Date = DateTime.MaxValue });
                notes.Add(new Note() { Name = "b", Category = "b1", Content = "b2", Date = DateTime.Now});
                notes.Add(new Note() { Name = "c", Category = "c1", Content = "c2", Date = DateTime.Now});
                notes.Add(new Note() { Name = "d", Category = "d1", Content = "d2", Date = DateTime.MinValue });
            }
            return notes;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
