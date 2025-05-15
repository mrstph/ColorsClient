using ColorsClient.Models;
using ColorsClient.ViewModels;
using ColorsClient.Handlers;
using Microsoft.Extensions.Logging;

namespace ColorsClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();

#endif
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IColorApiService, ColorApiService>();
            builder.Services.AddSingleton<TokenHandler>();
            builder.Services.AddHttpClient("AuthorizedClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
            }).
                AddHttpMessageHandler<TokenHandler>();
            builder.Services.AddTransient<ColorPalettesViewModel>();
            builder.Services.AddTransient<ColorsPalettes>();

            return builder.Build();
        }
    }
}
