using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RaceLibrary.Repositories;
using RaceLibrary.Services;
using RaceTrace.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace RaceTrace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost {get; private set;}

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    // https://www.youtube.com/watch?v=dLR_D2IJE1M
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddScoped<IRaceService, RaceService>();
                    services.AddScoped<IRaceRepository, RaceRepository>();
                    services.AddSingleton<MainViewModel>();

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var startupWindow = AppHost.Services.GetRequiredService<MainWindow>();
            startupWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();

            base.OnExit(e);
        }
    }

}
