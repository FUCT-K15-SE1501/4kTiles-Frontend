using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.Report;
using _4kTiles_Frontend.Services.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Library.Report
{
    internal class ReportmodelViewModel : ObservableObject
    {
        public StatusDTO _statusDTO;
        public ReportModel ReportIns { get; set; } = new ReportModel();

        public IFkTilesClient Client { get; set; }

        public StatusDTO StatusDTO
        {
            get { return _statusDTO; }
            set
            {
                _statusDTO = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand UpdateReportStatusCommand { get; set; }

        public ReportmodelViewModel()
        {
            Client = FkTilesClient.Client;
            StatusDTO = new();

            UpdateReportStatusCommand = new RelayCommand(async o =>
            {
                var messageBox = MessageBox.Show($"{ReportIns.ReportReason}", $"Apply for report {ReportIns.ReportId} ?", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
                if (messageBox == MessageBoxResult.Yes)
                {
                    var result = await UpdateReportStatus(id: ReportIns.ReportId, status: true);
                    if (result == true)
                    {
                        MessageBox.Show("Report applied!");
                    }

                }
                else if (messageBox == MessageBoxResult.No)
                {
                    StatusDTO.Status = false;
                    var result = await UpdateReportStatus(id: ReportIns.ReportId, status: false);
                    if (result == true)
                    {
                        MessageBox.Show("Report rejected!");
                    }
                }
            });
        }

        public async Task<bool> UpdateReportStatus(int id, bool status) 
        {
            if(id == 0)
            {
                return false;
            }

            var response = await Client.RequestAsync<string?>($"SongReports/{id}?status={status}", RequestMethod.PUT, new ());

            if (response == null || response.IsError)
            {
                _ = MessageBox.Show(response?.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }

    public class StatusDTO
    {
        public bool Status { get; set; }
    }
}
