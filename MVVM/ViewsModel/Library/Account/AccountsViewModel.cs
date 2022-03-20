using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.Account;
using _4kTiles_Frontend.Services.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Library.Account
{
    internal class AccountsViewModel : ObservableObject
    {
        private object _currentView;
        private AccountModel _accountModel;
        private List<AccountmodelViewModel> _accountList;
        private string _searchText;
        private int _selectedIndex;

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

        public AccountModel AccountModel
        {
            get => _accountModel;
            set
            {
                _accountModel = value;
                OnPropertyChanged();
            }
        }

        public List<AccountmodelViewModel> AccountList
        {
            get => _accountList;
            set
            {
                _accountList = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public RelayCommand DeactivateCommand { get; set; }

        public RelayCommand NextCommand { get; set; }
        public RelayCommand PrevCommand { get; set; }

        public RelayCommand SearchCommand { get; set; }

        public AccountsViewModel()
        {
            Client = FkTilesClient.Client;
            AccountList = new();

            Task.Run(async () =>
            {
                SelectedIndex = 1;
                await GetList(SelectedIndex);
            });

            NextCommand = new RelayCommand(async o =>
            {
                if (AccountList.Count > 0)
                {
                    
                    await GetList(page: SelectedIndex + 1, searchText: SearchText);
                }
            });
            PrevCommand = new RelayCommand(async o =>
            {
                if (AccountList.Count > 0)
                {
                    if (SelectedIndex == 1)
                        return;
                    await GetList(page: SelectedIndex - 1, searchText: SearchText);
                }
            });
            SearchCommand = new RelayCommand(async o =>
            {
                await GetList(page: 1, searchText: SearchText);
            });

            
        }

        public async Task<bool> GetList(int page = 1, string searchText = "")
        {
            var accountResponse = await Client.RequestAsync<List<AccountModel>>($"Account/All?pageNumber={page}&pageSize={4}&searchString={searchText}");

            if (accountResponse == null || accountResponse.IsError)
            {
                return false;
            }

            var list = new List<AccountmodelViewModel>();
            if (accountResponse.Data.Count > 0)
            {

                foreach (var account in accountResponse.Data)
                {
                    var newAccountItem = new AccountmodelViewModel
                    {
                        AccountIns = account
                    };

                    list.Add(newAccountItem);
                }
                AccountList = list;
                SelectedIndex = page;
            }
            return true;
        }

        
    }

}
