using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.ViewsModel.Library.Account;
using _4kTiles_Frontend.MVVM.ViewsModel.Library.Private;
using _4kTiles_Frontend.MVVM.ViewsModel.Library.Public;
using _4kTiles_Frontend.MVVM.ViewsModel.Library.Report;
using _4kTiles_Frontend.Services.ApiClient;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Library
{
    internal class LibraryViewModel : ObservableObject
    {
        #region fields
        private object _currentView;
        #endregion

        #region properties
        public IFkTilesClient Client { get; set; }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public PublicViewModel PublicViewModel { get; set; }
        public PrivateViewModel PrivateViewModel { get; set; }
        public AccountsViewModel AccountsViewModel { get; set; }
        public ReportsViewModel ReportsViewModel { get; set; }

        public RelayCommand PublicCommand { get; set; }
        public RelayCommand PrivateCommand { get; set; }
        public RelayCommand AccountsCommand { get; set; }
        public RelayCommand ReportsCommand { get; set; }

        #endregion
        public LibraryViewModel()
        {
            Client = FkTilesClient.Client;

            PublicViewModel = new();
            PrivateViewModel = new();
            AccountsViewModel = new();
            ReportsViewModel = new();

            CurrentView = PublicViewModel;

            PublicCommand = new RelayCommand(o =>
            {
                CurrentView = PublicViewModel;
            });

            PrivateCommand = new RelayCommand(o =>
            {
                CurrentView = PrivateViewModel;
            });

            AccountsCommand = new RelayCommand(o =>
            {
                CurrentView = AccountsViewModel;
            });

            ReportsCommand = new RelayCommand(o =>
            {
                CurrentView = ReportsViewModel;
            });
        }
    }
}
