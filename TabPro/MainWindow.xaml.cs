using Firebase.Auth;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace TabPro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FirebaseAuthClient _authClient;

        public MainWindow(FirebaseAuthClient authClient)
        {
            InitializeComponent();
            _authClient = authClient;
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Password;

            try
            {
                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(email, password);
                var user = userCredential.User;
                MessageBox.Show($"Welcome {user.Info.DisplayName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sign-in error: {ex.Message}");
            }
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Password;
            string displayName = DisplayNameTextBox.Text;

            try
            {
                var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password, displayName);
                var user = userCredential.User;
                MessageBox.Show($"User created: {user.Info.DisplayName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sign-up error: {ex.Message}");
            }
        }
    }
}