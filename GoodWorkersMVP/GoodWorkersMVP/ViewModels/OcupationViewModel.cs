using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GoodWorkersMVP.ViewModels
{
    public class OcupationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ApiService apiService;
        DialogHelper dialogHelper;

        ObservableCollection<Ocupation> _ocupations;

        public ObservableCollection<Ocupation> Ocupations
        {
            get { return _ocupations; }
            set
            {
                if (_ocupations != value)
                {
                    _ocupations = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ocupations)));
                }
            }
        } 

        public OcupationViewModel()
        {
            apiService = new ApiService();
            dialogHelper = new DialogHelper();
            LoadOcupations();
        }

        async void LoadOcupations()
        {
            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await dialogHelper.ShowMessage("Error", connection.Message);
                return;
            }

            //var mainViewModel = MainViewModel.GetInstance()
            var response = await apiService.GetList<Ocupation>(
                "https://goodworkers-api.herokuapp.com/",
                "api/",
                "ocupations");

            if (!response.IsSuccess)
            {
                await dialogHelper.ShowMessage("Error", response.Message);
                return;
            }

            var ocupationList = (List<Ocupation>)response.Result;
            Ocupations = new ObservableCollection<Ocupation>(ocupationList.OrderBy(o => o.OcupationName));
        }
    }
}
