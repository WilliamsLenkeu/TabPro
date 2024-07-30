using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace TabPro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private FirebaseAuthProvider _authProvider;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    string firebaseApiKey = context.Configuration.GetValue<string>("FIREBASE_API_KEY");

                    if (string.IsNullOrEmpty(firebaseApiKey))
                    {
                        throw new InvalidOperationException("FIREBASE_API_KEY is missing in the configuration.");
                    }

                    _authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey));
                    services.AddSingleton(_authProvider);

                    services.AddSingleton<MainWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            await CreateUserWithEmailAndPassword("test@gmail.com", "Test123!");

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }

        private async Task CreateUserWithEmailAndPassword(string email, string password)
        {
            var authLink = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            Console.WriteLine($"Successfully created new user: {authLink.User.LocalId}");
        }
    }
}
