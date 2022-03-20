using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.Account;
using _4kTiles_Frontend.MVVM.ViewsModel.Library.Account;
using _4kTiles_Frontend.Services.ApiClient;
using System.Threading.Tasks;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Settings
{
    internal class SettingsViewModel : ObservableObject
    {
        private AccountModel _accountModel;

        public IFkTilesClient Client { get; set; }

        public AccountModel AccountModel
        {
            get => _accountModel;
            set
            {
                _accountModel = value;
                OnPropertyChanged();
            }
        }

        

        public SettingsViewModel()
        {
            Client = FkTilesClient.Client;
            AccountModel = new();

            Task.Run(async () =>
            {
                await GetAccountInfo(Client.GetAccount().AccountId);
            });
        }

        public async Task<bool> GetAccountInfo(int id)
        {
            var response = await Client.RequestAsync<AccountModel>($"Account/Account/{id}");
            if (response.IsError || response == null)
            {
                return false;
            }

            if(response.Data != null)
            {
                AccountModel = response.Data;
            }

            return true;
        }
    }
}
