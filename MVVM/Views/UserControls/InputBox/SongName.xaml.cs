using System.Windows.Controls;

namespace _4kTiles_Frontend.MVVM.Views.UserControls.InputBox
{
    /// <summary>
    /// Interaction logic for SongName.xaml
    /// </summary>
    public partial class SongName : UserControl
    {
        public SongName()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string NameSong { get; set; }
        public string SelectedRow { get; set; }

    }
}
