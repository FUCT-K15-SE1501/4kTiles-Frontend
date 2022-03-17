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
        private ReportBody _reportBody;
        private object _currentView;
        private List<ReportmodelViewModel> _reportList;

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

        public ReportBody ReportBody
        {
            get => _reportBody;
            set
            {
                _reportBody = value;
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
        #endregion

        public RelayCommand UploadReportRequest { get; set; }

        public ReportsViewModel()
        {
            Client = FkTilesClient.Client;
            ReportList = new();

            UploadReportRequest = new(o =>
            {
                _ = Task.Run(async () =>
                {
                    await CreateReport();
                });
            });


            Task.Run(async () =>
            {
                await GetList();
            });
        }

        public async Task<bool> GetList()
        {
            var reportResponse = await Client.RequestAsync<List<ReportModel>>("SongReports");

            if (reportResponse == null || reportResponse.IsError)
            {
                return false;
            }

            var list = new List<ReportmodelViewModel>();
            foreach (var report in reportResponse.Data)
            {
                var newReportItem = new ReportmodelViewModel
                {
                    ReportIns = report
                };

                list.Add(newReportItem);
            }

            ReportList = list;

            return true;
        }

        public async Task<bool> CreateReport()
        {
            var response = await Client.RequestAsync<ReportModel>("SongReports/Create", RequestMethod.POST, ReportBody);
            if (response == null || response.IsError)
            {
                _ = MessageBox.Show(response?.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MessageBox.Show("Report sent!");
            return true;
        }
    }
}

public class ReportBody
{
    public int SongId { get; set; }
    public int AccountId { get; set; }
    public string? ReportTitle { get; set; }
    public string? ReportReason { get; set; }
}
