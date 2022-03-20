using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.Library.Items;
using _4kTiles_Frontend.MVVM.ViewsModel.Library.Public;
using _4kTiles_Frontend.Services.ApiClient;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Library.Private
{
    internal class PrivateViewModel : ObservableObject
    {
        #region fields
        private object _currentView;
        private List<SongOverviewViewModel> _songsList;
        private int _selectedIndex;
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


        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NextCommand { get; set; }
        public RelayCommand PrevCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public PrivateViewModel()
        {
            Client = FkTilesClient.Client;
            SongsList = new();

            Task.Run(async () =>
            {
                SelectedIndex = 1;
                await GetList(SelectedIndex);
            });

            NextCommand = new RelayCommand(async o =>
            {
                if (SongsList.Count > 0)
                {
                    await GetList(page: SelectedIndex + 1);
                }
            });
            PrevCommand = new RelayCommand(async o =>
            {
                if (SongsList.Count > 0)
                {
                    if (SelectedIndex == 1)
                        return;
                    await GetList(page: SelectedIndex - 1);
                }
            });

        }

        public async Task<bool> GetList(int page = 1)
        {
            var songsResponse = await Client.RequestAsync<List<SongOverviewModel>>($"Library/private?pageNumber={page}&pageSize={4}");

            if (songsResponse == null || songsResponse.IsError)
            {
                return false;
            }



            var list = new List<SongOverviewViewModel>();
            if (songsResponse.Data.Count > 0)
            {
                foreach (var song in songsResponse.Data)
                {
                    var newSongItem = new SongOverviewViewModel
                    {
                        SongOverview = song,
                    };

                    list.Add(newSongItem);
                }
                SongsList = list;
                SelectedIndex = page;
            }
            return true;
        }

        
    }
}
