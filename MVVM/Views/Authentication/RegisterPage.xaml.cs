using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

using _4kTiles_Frontend.MVVM.Models.Auth;
using _4kTiles_Frontend.MVVM.Views.Common;
using _4kTiles_Frontend.Services.ApiClient;

namespace _4kTiles_Frontend.MVVM.Views.Authentication
{
    /// <summary>
    /// Interaction logic for Register_Page.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {
        IFkTilesClient Client { get; set; }
        public RegisterPage()
        {
            InitializeComponent();

            Client = FkTilesClient.Client;
        }

        private void Minimize_Window(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_Window(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private async void RegisBtn_Click(object sender, RoutedEventArgs e)
        {
            var spinner = new Spinner();
            spinner.Show();

            if (string.IsNullOrWhiteSpace(emailBox.Text))
            {
                MessageBox.Show("Username can't be empty");
                spinner.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(usernameBox.Text))
            {
                MessageBox.Show("Username can't be empty");
                spinner.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(pwdBox.Password))
            {
                MessageBox.Show("Password cant be empty");
                spinner.Close();
                return;
            }

            var dao = new RegisterModel()
            {
                UserName = usernameBox.Text,
                Email = emailBox.Text,
                Password = pwdBox.Password
            };

            var registerTask = await Client.RequestAsync<string>("account/registeruser", RequestMethod.POST, dao);

            if (registerTask != null)
            {
                MessageBox.Show("Account registered. please check your mail box!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                var loginPage = new LoginPage();
                loginPage.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Failed to register", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            spinner.Close();
        }

        private void LoginPage_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            Close();
        }

        private void ConfirmPwd_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(confirmPwd.Password))
            {
                confirmPwd.Clear();
            }
            else if (confirmPwd.Password != pwdBox.Password)
            {
                Trace.WriteLine("Please confirm your password");
            }
        }

        private void Username_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(usernameBox.Text))
            {
                usernameBox.Clear();
            }
        }

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(emailBox.Text))
            {
                emailBox.Clear();
            }
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(pwdBox.Password))
            {
                pwdBox.Clear();
            }
        }
    }
}
