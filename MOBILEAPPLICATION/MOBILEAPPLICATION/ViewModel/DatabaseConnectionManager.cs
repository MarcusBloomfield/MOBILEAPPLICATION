using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MOBILEAPPLICATION.Model;
using System.Collections.ObjectModel;

namespace MOBILEAPPLICATION.ViewModel
{
    public class DatabaseConnectionManager
    {
        // stored user details 
        static string savedUserName;
        static string savedPassWord;
        // API keys used to access the api to database
        static readonly string subkey = null;
        static readonly string keyType = null;
        static readonly string apiURL = null;
        public static async Task<string> Login(string userName, string passWord)
        {
            // this function returns the notes from the data base 
            savedUserName = userName;
            savedPassWord = passWord;
            if (!string.IsNullOrEmpty(savedUserName) && !string.IsNullOrEmpty(savedPassWord))
            {
                // assigns static user name and pass word on login
                // new api url with correct http adress
                string loginApiUrl = apiURL + "/Login";
                // creates payload
                var payload = new { loginUserName = userName, loginPassword = passWord };
                StringContent httpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    // assigns keys
                    httpClient.DefaultRequestHeaders.Add(keyType, subkey);
                    // posts the paylod to the http adress 
                    // then awaits the response from the api which grabs it from the database
                    // returns the notes
                    var httpResponse = await httpClient.PostAsync(loginApiUrl, httpContent);

                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        return responseContent;
                    }
                }
            }
            return string.Empty;
        }
        public static async Task UpdateNote(ObservableCollection<Note> note)
        {
            if (!string.IsNullOrEmpty(savedUserName) && !string.IsNullOrEmpty(savedPassWord))
            {
                // converts the notes to json, then sends them to the database through the api
                // new api url with correct http adress
                var updateApiUrl = apiURL + "/UpdateNotes";
                var payload = new { note = JsonConvert.SerializeObject(note), userName = savedUserName, passWord = savedPassWord };
                StringContent httpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    // posts the notes to the database trhough the api
                    httpClient.DefaultRequestHeaders.Add(keyType, subkey);
                    await httpClient.PostAsync(updateApiUrl, httpContent);
                }
            }
        }
        public static async Task<bool> CreateAccount(string userName, string passWord)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord))
            {
                // this function returns the notes from the data base 
                savedUserName = userName;
                savedPassWord = passWord;
                // assigns static user name and pass word on login
                // new api url with correct http adress
                var updateApiUrl = apiURL + "/CreateAccount";
                // creates payload
                var payload = new { userName = savedPassWord, passWord = savedPassWord };
                StringContent httpContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    // sends the new login information to the datbase
                    // upon successful creation of the new account it returns a string boolean to process the request
                    httpClient.DefaultRequestHeaders.Add(keyType, subkey);
                    var httpResponse = await httpClient.PostAsync(updateApiUrl, httpContent);

                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        try
                        {
                            return Boolean.Parse(responseContent);
                        }
                        catch (NullReferenceException ex)
                        {
                            Console.WriteLine(ex.Message);
                            return false;
                        }
                    }
                }
            }
            return false;
        }
    }
}
