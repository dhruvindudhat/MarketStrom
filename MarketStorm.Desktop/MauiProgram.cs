using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using MarketStrom.UIComponents.Services;

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
            builder.Services.AddBlazorise(options => { options.Immediate = true; }).AddBootstrapProviders().AddFontAwesomeIcons();
            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            builder.Services.AddSingleton<DatabaseService>();
            //builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}