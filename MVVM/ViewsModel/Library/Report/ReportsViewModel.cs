using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.Report;
using _4kTiles_Frontend.Services.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Library.Report
{
    internal class ReportsViewModel : ObservableObject
    {
        private object _currentView;
        private List<ReportmodelViewModel> _reportList;
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


        public List<ReportmodelViewModel> ReportList
        {
            get => _reportList;
            set
            {
                _reportList = value;
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


        //command for pagination and search
        public RelayCommand NextCommand { get; set; }
        public RelayCommand PrevCommand { get; set; }

        public RelayCommand SearchCommand { get; set; }

        public ReportsViewModel()
        {
            Client = FkTilesClient.Client;
            ReportList = new();

            Task.Run(async () =>
            {
                SelectedIndex = 1;
                await GetList(SelectedIndex);
            });

            NextCommand = new RelayCommand(async o =>
            {
                if (ReportList.Count > 0)
                {
                    
                    await GetList(page: SelectedIndex + 1);
                }
            });
            PrevCommand = new RelayCommand(async o =>
            {
                if (ReportList.Count > 0)
                {
                    if (SelectedIndex == 1)
                        return;
                    await GetList(page: SelectedIndex - 1);
                }
            });

        }

        public async Task<bool> GetList(int page = 1)
        {
            var reportResponse = await Client.RequestAsync<List<ReportModel>>($"SongReports?pageNumber={page}&pageSize={4}");

            if (reportResponse == null || reportResponse.IsError)
            {
                return false;
            }

            var list = new List<ReportmodelViewModel>();
            if(reportResponse.Data.Count > 0)
            {

            
                foreach (var report in reportResponse.Data)
                {
                    var newReportItem = new ReportmodelViewModel
                    {
                        ReportIns = report
                    };

                    list.Add(newReportItem);
                }

                ReportList = list;
                SelectedIndex = page;
            }


            return true;
        }

    }
}

