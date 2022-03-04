using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

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
            this.Close();
        }

        private void RegisBtn_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("Registered!");
        }

        private void LoginPage_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
        }

        private void ConfirmPwd_GotFocus(object sender, RoutedEventArgs e)
        {
            if (confirmPwd.Password == "Password")
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
            if (usernameBox.Text == "Username")
            {
                usernameBox.Clear();
            }
        }

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            if (emailBox.Text == "Email")
            {
                emailBox.Clear();
            }
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (pwdBox.Password == "Password")
            {
                pwdBox.Clear();
            }
        }
    }
}
