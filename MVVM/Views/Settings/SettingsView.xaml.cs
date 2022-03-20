using _4kTiles_Frontend.MVVM.Models.Auth;
using _4kTiles_Frontend.MVVM.Views.Authentication;
using _4kTiles_Frontend.MVVM.Views.Common;
using _4kTiles_Frontend.Services.ApiClient;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace _4kTiles_Frontend.MVVM.Views.Settings
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        IFkTilesClient Client { get; set; }
        public SettingsView()
        {
            InitializeComponent();
            Client = FkTilesClient.Client;
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            LoginPage loginPage = new();
            if (Client.IsUserLoggedIn())
            {
                window.Close();
                loginPage.Show();
            }


        }
    }
}
