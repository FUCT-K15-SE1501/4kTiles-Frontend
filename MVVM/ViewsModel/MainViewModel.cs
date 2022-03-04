using System.Diagnostics;

using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.ViewsModel.Home;
using _4kTiles_Frontend.MVVM.ViewsModel.Library;
using _4kTiles_Frontend.MVVM.ViewsModel.Settings;

namespace _4kTiles_Frontend.MVVM.ViewsModel
{
    internal class MainViewModel : ObservableObject
    {
        private object _mainViewModel;

        public object CurrentView
        {
            get => _mainViewModel;
            set
            {
                if (_mainViewModel != value)
                {
                    _mainViewModel = value;
                    OnPropertyChanged();
                }
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

        public MainViewModel()
        {
            HomeViewModel = new HomeViewModel();
            LibraryViewModel = new LibraryViewModel();
            SettingsViewModel = new SettingsViewModel();

            CurrentView = HomeViewModel;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeViewModel;
            });

            LibraryViewCommand = new RelayCommand(o =>
            {
                CurrentView = LibraryViewModel;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsViewModel;
            });
        }

    }
}
