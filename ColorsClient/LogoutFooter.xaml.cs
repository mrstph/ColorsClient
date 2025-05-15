using ColorsClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ColorsClient
{
    public partial class LogoutFooter : ContentView
    {
        public LogoutFooter()
        {
            InitializeComponent();
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            try
            {
                var accessToken = await SecureStorage.GetAsync("access_token");

                if (!string.IsNullOrEmpty(accessToken))
                {
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var response = await client.PostAsync("https://localhost:5001/api/auth/logout", null);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Erreur lors du logout : {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception lors du logout : {ex.Message}");
            }
            finally
            {
                SecureStorage.Remove("access_token");
                SecureStorage.Remove("refresh_token");

                await Shell.Current.GoToAsync("///Login");
            }
        }
    }
}