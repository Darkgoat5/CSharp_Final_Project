using Microsoft.Extensions.Logging;
using MovieTracker.MovieService;
using MovieTracker.ViewModels;
using Microsoft.Maui;

namespace MovieTracker
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

            // make HTTP client for whole app to access api
            builder.Services.AddSingleton<HttpClient>();

            // use 1 movieservice across whole app
            builder.Services.AddSingleton<IMovieService, MovieService.MovieService>();

            // allows DI to inject movieservice into viewmodels in the constructor of the viewmodel
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<AddMoviePageViewModel>();

            // needed for DI to inject viewmodels into pages
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AddMoviePage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
