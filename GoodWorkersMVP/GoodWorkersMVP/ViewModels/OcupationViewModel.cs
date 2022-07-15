using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class OcupationViewModel : BaseViewModel
    {
        ApiService apiService;
        NavigationService navigationService;

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
            navigationService = new NavigationService();
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
                    Languages.ErrorTitleDialog,
                    connection.Message,
                    Languages.BtnAcceptDialog);

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
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return;
            }

            var ocupationList = (List<Ocupation>)response.Result;
            Ocupations = new ObservableCollection<Ocupation>(ocupationList.OrderBy(o => o.OcupationName));

            await navigationService.BackOnLogin();

            navigationService.SetMainPage("MasterPage");

            IsRefreshing = false;
        }
    }
}
