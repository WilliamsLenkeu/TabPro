using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Windows;
using Firebase.Auth.Providers;
using Firebase.Auth;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;

namespace TabPro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Configuration from appsettings.json or environment variables
                    var firebaseApiKey = context.Configuration.GetValue<string>("FIREBASE_API_KEY");
                    var firestoreProjectId = context.Configuration.GetValue<string>("FIRESTORE_PROJECT_ID");

                    // Configure Firebase Authentication
                    var config = new FirebaseAuthConfig
                    {
                        ApiKey = firebaseApiKey,
                        AuthDomain = "datamanager-e94af.firebaseapp.com",
                        Providers = new FirebaseAuthProvider[]
                        {
                            new EmailProvider()
                        }
                    };

                    // Configure Firestore using the service account credentials
                    var credentialsPath = "service-account-file.json"; // Ensure this path is correct
                    var credential = GoogleCredential.FromFile(credentialsPath);

                    // Create FirestoreDb instance with credentials
                    var firestoreDb = FirestoreDb.Create(firestoreProjectId, new FirestoreClientBuilder
                    {
                        ChannelCredentials = credential.ToChannelCredentials()
                    });

                    // Register services
                    services.AddSingleton(new FirebaseAuthClient(config));
                    services.AddSingleton(firestoreDb);
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
