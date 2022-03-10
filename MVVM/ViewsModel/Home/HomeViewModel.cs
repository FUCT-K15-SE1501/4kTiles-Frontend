
using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.Services.ApiClient;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Home
{
    internal class HomeViewModel : ObservableObject
    {
        IFkTilesClient Client { get; set; }

        #region fields
        private string _userName;
        #endregion

        #region properties
        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public HomeViewModel()
        {
            Client = FkTilesClient.Client;

            UserName = Client.GetAccount().UserName;
        }
    }
}
