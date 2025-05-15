namespace ColorsClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            CheckAuthentication();
        }

        private async void CheckAuthentication()
        {
            var token = await SecureStorage.GetAsync("access_token");

            if (string.IsNullOrEmpty(token))
            {
                await GoToAsync("///Login");
            }
        }
    }
}
