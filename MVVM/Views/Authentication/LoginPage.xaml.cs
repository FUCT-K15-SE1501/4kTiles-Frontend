using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using _4kTiles_Frontend.MVVM.Models.Auth;
using _4kTiles_Frontend.MVVM.Views.Common;
using _4kTiles_Frontend.MVVM.Views.Editor;
using _4kTiles_Frontend.Services.ApiClient;

namespace _4kTiles_Frontend.MVVM.Views.Authentication
{
    /// <summary>
    /// Interaction logic for Login_Page.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        IFkTilesClient Client { get; set; }

        public LoginPage()
        {
            InitializeComponent();

            Client = FkTilesClient.Client;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ForgotPasswordBtn(object sender, RoutedEventArgs e)
        {

        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var spinner = new Spinner();
            spinner.Show();

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

            LoginModel dao = new()
            {
                Email = usernameBox.Text,
                Password = pwdBox.Password
            };

            bool loginTask = await Client.Login(dao);

            if (loginTask && Client.IsUserLoggedIn())
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Failed to login", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            spinner.Close();
            Close();
        }

        private void Close_Window(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Minimize_Window(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            registerPage.Show();
            Close();
        }


        private void LoginUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(usernameBox.Text))
            {
                usernameBox.Clear();
            }
        }

        private void LoginPwd_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(pwdBox.Password))
            {
                pwdBox.Clear();
            }
        }
    }
}
