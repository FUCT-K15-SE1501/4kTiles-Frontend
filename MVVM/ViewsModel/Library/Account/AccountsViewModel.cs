using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.Account;
using _4kTiles_Frontend.Services.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Library.Account
{
    internal class AccountsViewModel : ObservableObject
    {
        private object _currentView;
        private List<AccountmodelViewModel> _accountList;


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

        public List<AccountmodelViewModel> AccountList
        {
            get => _accountList;
            set
            {
                _accountList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public AccountsViewModel()
        {
            Client = FkTilesClient.Client;
            AccountList = new();

            Task.Run(async () =>
            {
                await GetList();
            });
        }

        public async Task<bool> GetList()
        {
            var accountResponse = await Client.RequestAsync<List<AccountModel>>("Account/All");

            if (accountResponse == null || accountResponse.IsError)
            {
                return false;
            }

            var list = new List<AccountmodelViewModel>();
            foreach (var account in accountResponse.Data)
            {
                var newAccountItem = new AccountmodelViewModel
                {
                    AccountIns = account
                };

                list.Add(newAccountItem);
            }

            AccountList = list;

            return true;
        }
    }
}
