using Firebase.Auth;
using System;
using System.Windows;

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
            _authClient = authClient;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            try
            {
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                var user = userCredential.User;
                MessageBox.Show($"Welcome {user.Info.DisplayName}");
                // Navigate to the main application window or dashboard
                MainWindow mainWindow = new MainWindow(_authClient);
                mainWindow.Show();
                this.Close(); // Close the login window
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sign-in error: {ex.Message}");
            }
        }

        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            if (isPasswordVisible)
            {
                PasswordBox.PasswordChar = '\0'; // Show password
            }
            else
            {
                PasswordBox.PasswordChar = '•'; // Hide password
            }
        }
    }
}
