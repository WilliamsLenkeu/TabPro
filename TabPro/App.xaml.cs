using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace TabPro
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    string firebaseApiKey = context.Configuration.GetValue<string>("FIREBASE_API_KEY");
                    var config = new FirebaseAuthConfig
                    {
                        ApiKey = firebaseApiKey,
                        AuthDomain = "datamanager-e94af.firebaseapp.com",
                        Providers = new FirebaseAuthProvider[]
                        {
                            new EmailProvider()
                        },
                        UserRepository = new FileUserRepository("FirebaseSample")
                    };

                    services.AddSingleton(new FirebaseAuthClient(config));

                    services.AddSingleton<MainWindow>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow = mainWindow;
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
