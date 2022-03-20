using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.Library.Items;
using _4kTiles_Frontend.MVVM.Views.UserControls;
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


        //pagination command
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
                    await GetList(page: SelectedIndex + 1, searchText: SearchText);
                }
            });
            PrevCommand = new RelayCommand(async o =>
            {
                if (SongsList.Count > 0)
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
            var songsResponse = await Client.RequestAsync<List<SongOverviewModel>>($"Library/search?pageNumber={page}&pageSize={4}&searchString={searchText}");

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
                        SongOverview = song
                    };

                    list.Add(newSongItem);
                }
                SongsList = list;
                SelectedIndex = page;
            }
            
            return true;
        }


        
    }

    //public class ReportDTO
    //{
    //    public int SongId { get; set; }
    //    public int AccountId { get; set; }
    //    public string ReportTitle { get; set; }
    //    public string ReportReason { get; set; }
    //}
}


