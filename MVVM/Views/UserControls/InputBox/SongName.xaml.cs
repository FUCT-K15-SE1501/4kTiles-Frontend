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
