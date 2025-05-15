using ColorsClient.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ColorsClient
{
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
            {
                await DisplayAlert("Erreur", "Les mots de passe ne correspondent pas", "OK");
                return;
            }

            var register = new RegisterDto
            {
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text
            };

            var json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var http = new HttpClient();
            var response = await http.PostAsync("https://localhost:5001/api/auth/register", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Succès", "Inscription réussie. Vous pouvez maintenant vous connecter.", "OK");
                await Shell.Current.GoToAsync("///Login");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Erreur", error, "OK");
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///Login");
        }
    }
}
