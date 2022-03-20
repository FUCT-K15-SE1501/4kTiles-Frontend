using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.Account;
using _4kTiles_Frontend.Services.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Library.Account
{
    internal class AccountmodelViewModel
    {
        public AccountModel AccountIns { get; set; } = new AccountModel();

        public IFkTilesClient Client { get; set; }

        public RelayCommand DeactivateCommand { get; set; }

        public AccountmodelViewModel()
        {
            Client = FkTilesClient.Client;


            DeactivateCommand = new RelayCommand(async o =>
            {
                var messageBoxResult = MessageBox.Show("Do you want to delete this account ?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    var result = await DeleteAccount(AccountIns.AccountId, "Delete an account");
                    if (result == true)
                    {
                        MessageBox.Show("Account Deactivated!");
                    }

                }
            });
        }

        public async Task<bool> DeleteAccount(int id, string? message)
        {
            if (id == 0)
            {
                return false;
            }

            var response = await Client.RequestAsync<string?>($"Account/Admin/{id}?message={message}", RequestMethod.DELETE);

            if (response == null || response.IsError)
            {
                _ = MessageBox.Show(response?.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            return true;
        }
    }

 
    
}
