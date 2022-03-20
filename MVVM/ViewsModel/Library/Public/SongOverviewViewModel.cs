using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _4kTiles_Frontend.Core.Functions;
using _4kTiles_Frontend.MVVM.Models.Library.Items;
using _4kTiles_Frontend.MVVM.Views.Library.Report;
using _4kTiles_Frontend.MVVM.Views.UserControls;
using _4kTiles_Frontend.MVVM.ViewsModel.Library.Report;
using _4kTiles_Frontend.Services.ApiClient;

namespace _4kTiles_Frontend.MVVM.ViewsModel.Library.Public
{
    internal class SongOverviewViewModel : ObservableObject
    {
        private ReportDTO _reportDTO;
        public SongOverviewModel SongOverview { get; set; }

        public IFkTilesClient Client { get; set; }

        public ReportDTO ReportDTO
        {
            get { return _reportDTO; }
            set
            {
                _reportDTO = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand OpenModalCommand { get; set; }
        public RelayCommand ReportCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public SongOverviewViewModel()
        {
            Client = FkTilesClient.Client;
            


            OpenModalCommand = new RelayCommand(o =>
            {
                var customModalDialog = new ReportModalDialog();
                customModalDialog.ReportDTO = new()
                {

                    AccountId = Client.GetAccount().AccountId,
                    SongId = SongOverview.SongId,
                };
                customModalDialog.Show();

                
            });

            DeleteCommand = new RelayCommand(async o =>
            {
                var messageBoxResult = MessageBox.Show("You want to delete this song?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning );
                if( messageBoxResult == MessageBoxResult.OK)
                {
                    var result = await DeleteSong(SongOverview.SongId);
                    if(result == true)
                    {
                        MessageBox.Show("Song deleted!");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Song deletion canceled!");
                }
                
            });

        }

        public async Task<bool> DeleteSong(int id)
        {
            if (id == 0)
            {
                return false;
            }

            var response = await Client.RequestAsync<string>($"Song/Delete/Admin/{id}", RequestMethod.DELETE);

            if (response == null || response.IsError)
            {
                _ = MessageBox.Show(response?.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            return true;
        }
    }
}
