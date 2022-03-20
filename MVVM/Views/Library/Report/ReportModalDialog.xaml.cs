using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.ViewsModel.Library.Report;
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

namespace _4kTiles_Frontend.MVVM.Views.Library.Report
{
    /// <summary>
    /// Interaction logic for ReportModalDialog.xaml
    /// </summary>
    public partial class ReportModalDialog : Window
    {
        public IFkTilesClient Client { get; set; }
        public ReportModalDialog()
        {
            InitializeComponent();
            DataContext = this;
            Client = FkTilesClient.Client;
        }

        
        public bool ButtonIsClicked = false;
        public ReportDTO ReportDTO { get; set; }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text == "Title")
            {
                txtTitle.Clear();
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            window?.Close();
            //this.Visibility = Visibility.Collapsed;
        }


        private void SubmitReport_Click(object sender, RoutedEventArgs e)
        {
            ButtonIsClicked = true;
            if(ButtonIsClicked == true)
            {
                var res = Task.Run(async () => await ReportSong());
                if (res.Result.Equals(true))
                {
                    this.Close();
                }
            }
        }

        public async Task<bool> ReportSong()
        {
            
            var response = await Client.RequestAsync<string>("SongReports/Create", RequestMethod.POST, ReportDTO);
            if (response == null || response.IsError)
            {
                _ = MessageBox.Show(response?.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            MessageBox.Show("Report sent !");
            return true;
        }
    }
}
