using _4kTiles_Frontend.MVVM.Views.Common;
using _4kTiles_Frontend.Services.ApiClient;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        IFkTilesClient Client { get; set; }
        public ForgotPassword()
        {
            InitializeComponent();
            Client = FkTilesClient.Client;
        }

        //minimize window
        private void Minimize_Window(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        // close window
        private void Close_Window(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private async void RecoverBtn_Click(object sender, RoutedEventArgs e)
        {
            var spinner = new Spinner();
            spinner.Show();

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email can't be empty");
                spinner.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(pwdBox.Password))
            {
                MessageBox.Show("Password can't be empty");
                spinner.Close();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtReset.Text))
            {
                MessageBox.Show("Recovery code required!");
                spinner.Close();
                return;
            }

            var dao = new
            {
                email = txtEmail.Text,
                password = pwdBox.Password,
                resetCode = txtReset.Text
            };

            var response = await Client.RequestAsync<string>("Account/Reset", RequestMethod.POST, dao);
            if (response != null)
            {
                MessageBox.Show("Password changed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                var loginPage = new LoginPage();
                loginPage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Email not exist or disabled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            spinner.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }

        //clear resetBox on focus
        private void txtReset_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtReset.Text))
            {
                txtReset.Clear();
            }
        }

        //clear password box on focus
        private void pwdBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(pwdBox.Password))
            {
                pwdBox.Clear();
            }
        }


        //clear emailBox on focus
        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmail.Clear();
            }
        }

        //send recover code to email
        private async void Sendcode_Click(object sender, RoutedEventArgs e)
        {
            var spinner = new Spinner();
            spinner.Show();

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("You need email to get recover code", "Error", MessageBoxButton.OK);
                spinner.Close();
                return;
            }

            var response = await Client.RequestAsync<string>($"Account/Reset/Create/?email={txtEmail.Text}", RequestMethod.POST, new());

            if (response != null)
            {
                MessageBox.Show("Success ! An email for recover has been sent to your email", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                MessageBox.Show("Email not exist or disabled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            spinner.Close();


        }

    }
}
