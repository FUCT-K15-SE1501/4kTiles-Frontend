
using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.ViewsModel.Home;
using _4kTiles_Frontend.MVVM.ViewsModel.Library;
using _4kTiles_Frontend.MVVM.ViewsModel.Settings;

namespace _4kTiles_Frontend.MVVM.ViewsModel
{
    internal class MainViewModel : ObservableObject
    {

        #region
        private object _currentView;
        private string _currentTitle;
        #endregion

        #region properties
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public string CurrentTitle
        {
            get => _currentTitle;
            set
            {
                _currentTitle = value;
                OnPropertyChanged();
            }
        }

        // View model
        public HomeViewModel HomeViewModel { get; set; }
        public LibraryViewModel LibraryViewModel { get; set; }
        public SettingsViewModel SettingsViewModel { get; set; }

        // Command
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand LibraryViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        #endregion

        public MainViewModel()
        {
            HomeViewModel = new HomeViewModel();
            LibraryViewModel = new LibraryViewModel();
            SettingsViewModel = new SettingsViewModel();

            CurrentView = HomeViewModel;
            CurrentTitle = "Home";

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeViewModel;
                CurrentTitle = "Home";
            });

            LibraryViewCommand = new RelayCommand(o =>
            {
                CurrentView = LibraryViewModel;
                CurrentTitle = "Library";
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsViewModel;
                CurrentTitle = "Profile";
            });
        }

    }
}
