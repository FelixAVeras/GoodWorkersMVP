using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Services;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class OcupationViewModel : BaseViewModel
    {
        ApiService apiService;

        private bool isRefreshing;
        ObservableCollection<Ocupation> _ocupations;

        public ObservableCollection<Ocupation> Ocupations
        {
            get { return _ocupations; }
            set { SetValue(ref _ocupations, value); }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetValue(ref isRefreshing, value); }
        }

        public OcupationViewModel()
        {
            apiService = new ApiService();
            LoadOcupations();
        }

        public ICommand RefreshCommand
        {
            get { return new RelayCommand(LoadOcupations); }
        }

        async void LoadOcupations()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    connection.Message,
                    Languages.AcceptDialogButton);

                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }

            //var mainViewModel = MainViewModel.GetInstance()
            var response = await apiService.GetList<Ocupation>(
                "https://goodworkers-api.herokuapp.com/",
                "api/",
                "ocupations");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    response.Message,
                    Languages.AcceptDialogButton);

                return;
            }

            var ocupationList = (List<Ocupation>)response.Result;
            Ocupations = new ObservableCollection<Ocupation>(ocupationList.OrderBy(o => o.OcupationName));

            IsRefreshing = false;
        }
    }
}
