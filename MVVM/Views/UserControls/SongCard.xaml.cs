using _4kTiles_Frontend.MVVM.Views.Authentication;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _4kTiles_Frontend.MVVM.Views.UserControls
{
    /// <summary>
    /// Interaction logic for SongCard.xaml
    /// </summary>
    public partial class SongCard : UserControl
    {
        
        public SongCard()
        {
            InitializeComponent();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow
            {
                Content = new CustomModalDialog(),
                Owner = Application.Current.MainWindow
            };

            mainWindow.Show();
        }

    }
}
