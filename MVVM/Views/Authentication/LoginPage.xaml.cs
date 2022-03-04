using _4kTiles_Frontend.DataObjects.DAO.Auth;
using _4kTiles_Frontend.MVVM.Views.Authentication;
using _4kTiles_Frontend.Services.ApiClient;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            if (string.IsNullOrWhiteSpace(usernameBox.Text))
            {
                MessageBox.Show("Username can't be empty");
                return;
            }

            if (string.IsNullOrWhiteSpace(pwdBox.Password))
            {
                MessageBox.Show("Password cant be empty");
                return;
            }

            LoginDAO dao = new()
            {
                Email = usernameBox.Text,
                Password = pwdBox.Password
            };

            bool loginTask = await Client.Login(dao);

            if (loginTask && Client.IsUserLoggedIn())
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
                Close();
            } else
            {
                MessageBox.Show("Failed to login", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
            registerPage.ShowDialog();
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
