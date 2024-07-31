using Firebase.Auth;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace TabPro
{
    /// <summary>
    /// Interaction logic for loginPage.xaml
    /// </summary>
    public partial class loginPage : Window
    {
        private bool isPasswordVisible = false;
        private readonly FirebaseAuthClient _authClient;

        public loginPage(FirebaseAuthClient authClient)
        {
            InitializeComponent();
            _authClient = authClient ?? throw new ArgumentNullException(nameof(authClient));
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            try
            {
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                var user = userCredential.User;
                MessageBox.Show($"Bienvenue {user.Info.DisplayName}");
                // Navigation vers la fenêtre principale ou le tableau de bord
                MainWindow mainWindow = new MainWindow(_authClient);
                mainWindow.Show();
                this.Close(); // Ferme la fenêtre de connexion
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion : {ex.Message}");
            }
        }

        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            // Malheureusement, PasswordBox ne permet pas directement de modifier la visibilité du mot de passe.
            // Vous pourriez envisager de remplacer PasswordBox par un TextBox avec un mode de texte caché si vous avez besoin de cette fonctionnalité.
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the registration page
            InscriptionPage signUpPage = new InscriptionPage(_authClient);
            signUpPage.Show();
            this.Close(); // Optionally close the login window if you want to navigate away
        }
    }
}
