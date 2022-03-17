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
        private string _searchText;
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

        public RelayCommand NextCommand { get; set; }
        public RelayCommand PrevCommand { get; set; }

        public RelayCommand SearchCommand { get; set; }
        #endregion

        public PublicViewModel()
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
                    SelectedIndex += 1;
                    await GetList(page: SelectedIndex, searchText: SearchText);
                }
            });
            PrevCommand = new RelayCommand(async o =>
            {
                if (SongsList.Count > 0)
                {
                    SelectedIndex -= 1;
                    await GetList(page: SelectedIndex, searchText: SearchText);
                }
            });
            SearchCommand = new RelayCommand(async o =>
            {
                await GetList(page: 1, searchText: SearchText);
            });
        }

        public async Task<bool> GetList(int page = 1, string searchText = "")
        {
            var songsResponse = await Client.RequestAsync<List<SongOverviewModel>>($"Library/search?pageNumber={page}&pageSize={4}&searchString={searchText}");

            if (songsResponse == null || songsResponse.IsError)
            {
                return false;
            }

            var list = new List<SongOverviewViewModel>();
            foreach (var song in songsResponse.Data)
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
