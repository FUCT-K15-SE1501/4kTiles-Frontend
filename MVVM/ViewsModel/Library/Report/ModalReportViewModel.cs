using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Views.UserControls;
using _4kTiles_Frontend.Services.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Library.Report
{
    internal class ModalReportViewModel : ObservableObject
    {

        //private object _currentView;
        //private ReportDTO _reportDTO;


        //public IFkTilesClient Client { get; set; }

        //public object CurrentView
        //{
        //    get { return _currentView; }
        //    set
        //    {
        //        _currentView = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public ReportDTO ReportDTO
        //{
        //    get { return _reportDTO; }
        //    set
        //    {
        //        _reportDTO = value;
        //        OnPropertyChanged();
        //    }

        //}

        //public RelayCommand ReportCommand { get; set; }


        //public ModalReportViewModel()
        //{
        //    Client = FkTilesClient.Client;


        //    ReportCommand = new(async o =>
        //   {
        //       await ReportSong();
        //   });

        //}

        //public async Task<bool> ReportSong()
        //{
        //    var response = await Client.RequestAsync<string>("SongReports/Create", RequestMethod.POST, ReportDTO);
        //    if (response == null || response.IsError)
        //    {
        //        _ = MessageBox.Show(response?.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return false;
        //    }
        //    MessageBox.Show("Report sent !");
        //    return true;
        //}

    }

    public class ReportDTO
    {
        public int SongId { get; set; }
        public int AccountId { get; set; }
        public string ReportTitle { get; set; }
        public string ReportReason { get; set; }
    }
}
