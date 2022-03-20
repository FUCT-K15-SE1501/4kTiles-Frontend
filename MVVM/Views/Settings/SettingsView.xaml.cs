using _4kTiles_Frontend.MVVM.Views.Authentication;
using _4kTiles_Frontend.MVVM.Views.Common;
using _4kTiles_Frontend.Services.ApiClient;
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

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            LoginPage loginPage = new LoginPage();
            window.Close();
            loginPage.Show();
            
            
        }
    }
}
