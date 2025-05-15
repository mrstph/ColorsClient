using ColorsClient.Models;
using Newtonsoft.Json;
using System.Text;

namespace ColorsClient
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var login = new LoginDto
            {
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text
            };

            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var http = new HttpClient();
            var response = await http.PostAsync("https://localhost:5001/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenData = JsonConvert.DeserializeObject<AuthResponse>(jsonResponse);

                await SecureStorage.SetAsync("access_token", tokenData.Token);
                await SecureStorage.SetAsync("refresh_token", tokenData.RefreshToken);

                var toto = await SecureStorage.GetAsync("access_token");

                await Shell.Current.GoToAsync("//ColorsPalettes");
            }
            else
            {
                await DisplayAlert("Erreur", "Email ou mot de passe incorrect", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Register");
        }
    }
}
