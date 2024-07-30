using Firebase.Auth;
using Google.Cloud.Firestore;
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
        private readonly FirestoreDb _firestoreDb;

        public MainWindow(FirebaseAuthClient authClient, FirestoreDb firestoreDb)
        {
            InitializeComponent();
            _authClient = authClient;
            _firestoreDb = firestoreDb;
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
                await SaveUserToFirestore(user.Uid, email, displayName);
                MessageBox.Show($"User created: {user.Info.DisplayName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sign-up error: {ex.Message}");
            }
        }

        private async Task SaveUserToFirestore(string userId, string email, string displayName)
        {
            var docRef = _firestoreDb.Collection("users").Document(userId);
            var user = new
            {
                Email = email,
                DisplayName = displayName
            };

            await docRef.SetAsync(user);
        }
    }
}
