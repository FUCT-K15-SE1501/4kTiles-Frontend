using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.Library.Items;
using _4kTiles_Frontend.Services.ApiClient;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Library.Public
{
    internal class PublicViewModel : ObservableObject
    {
        #region fields
        private object _currentView;
        private List<SongOverviewViewModel> _songsList;
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

        public List<SongOverviewViewModel> SongsList
        {
            get => _songsList;
            set
            {
                _songsList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public PublicViewModel()
        {
            Client = FkTilesClient.Client;
            SongsList = new();

            Task.Run(async () =>
            {
                await GetList();
            });
        }

        public async Task<bool> GetList()
        {
            var songsResponse = await Client.RequestAsync<List<SongOverviewModel>>("Library");

            if (songsResponse == null || songsResponse.IsError)
            {
                return false;
            }

            var list = new List<SongOverviewViewModel>();
            foreach(var song in songsResponse.Data)
            {
                var newSongItem = new SongOverviewViewModel
                {
                    SongOverview = song
                };

                list.Add(newSongItem);
            }

            SongsList = list;

            return true;
        }
    }
}
