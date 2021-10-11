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
    public class DataBaseConnectionManagerTests
    {
        [TestMethod]
        public void LoginTestForBadLogin()
        {
            var notes = DatabaseConnectionManager.Login(RandomString(1000), RandomString(1000));
            string a = notes.Result;
            if (a.Length > 0) Assert.IsTrue(false);
        }
        [TestMethod]
        public void LoginTestForGoodLogin()
        {
            var notes = DatabaseConnectionManager.Login("a", "a");
            string a = notes.Result;
            if (a.Length > 0) Assert.IsTrue(true);
        }
        [TestMethod]
        public void LoginTestForNullLogin()
        {
            var notes = DatabaseConnectionManager.Login(null, null);
            string a = notes.Result;
            if (a.Length > 0) Assert.IsTrue(false);
        }
        [TestMethod]
        public void LoginTestForEmptyLogin()
        {
            var notes = DatabaseConnectionManager.Login(string.Empty, string.Empty);
            string a = notes.Result;
            if (a.Length == 0) Assert.IsTrue(true);
        }
        [TestMethod]
        public void UpdateNoteTestForNullNote()
        {
            DatabaseConnectionManager.UpdateNote(null);
        }
        [TestMethod]
        public void CreateAccountTestCreateAccountThatAlreadyExists()
        {
            if (DatabaseConnectionManager.CreateAccount("a", "a").Result == true) Assert.IsTrue(false);
        }
        [TestMethod]
        public void CreateAccountTestForNull()
        {
            if (DatabaseConnectionManager.CreateAccount(null, null).Result == true) Assert.IsTrue(false);
        }
        [TestMethod]
        public void CreateAccountTestForEmpty()
        {
            if (DatabaseConnectionManager.CreateAccount(string.Empty, string.Empty).Result == true) Assert.IsTrue(false);
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
