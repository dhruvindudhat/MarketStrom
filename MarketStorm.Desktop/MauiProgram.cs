using MarketStorm.Desktop.Data;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace MarketStorm.Desktop
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
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            //builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}