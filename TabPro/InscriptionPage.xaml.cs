using Firebase.Auth;
using System;
using System.Windows;

namespace TabPro
{
    /// <summary>
    /// Interaction logic for InscriptionPage.xaml
    /// </summary>
    public partial class InscriptionPage : Window
    {
        private readonly FirebaseAuthClient _authClient;

        public InscriptionPage(FirebaseAuthClient authClient)
        {
            InitializeComponent();
            _authClient = authClient ?? throw new ArgumentNullException(nameof(authClient));
        }

        private void RegionComboBox_DropDownOpened(object sender, EventArgs e)
        {
            RegionComboBox.Items.Clear(); // Efface les éléments existants

            string[] regions = new string[]
            {
                "Adamaoua",
                "Centre",
                "Est",
                "Extrême-Nord",
                "Littoral",
                "Nord",
                "Nord-Ouest",
                "Ouest",
                "Sud",
                "Sud-Ouest"
            };

            foreach (var region in regions)
            {
                RegionComboBox.Items.Add(region);
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string username = UsernameTextBox.Text;
            string phone = PhoneTextBox.Text;
            string region = RegionComboBox.SelectedItem as string;
            string city = CityTextBox.Text;
            string poste = posteTextBox.Text;

            if (password != confirmPassword)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.");
                return;
            }

            try
            {
                var userCredential = await _authClient.CreateUserWithEmailAndPasswordAsync(email, password, username);
                var user = userCredential.User;

                // Optionally, you could store additional information such as phone, region, etc. in your database.

                MessageBox.Show($"Utilisateur créé : {user.Info.DisplayName}");
                // Redirection vers la page de connexion ou autre page principale après l'inscription réussie
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur d'inscription : {ex.Message}");
            }
        }
    }
}
