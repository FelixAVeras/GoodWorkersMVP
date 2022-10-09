using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
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

        private bool isRefreshing;
        // private string filter;
        private ObservableCollection<Ocupation> ocupations;

        public ObservableCollection<Ocupation> Ocupations
        {
            get => ocupations;
            set => SetValue(ref ocupations, value);
        }

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetValue(ref isRefreshing, value);
        }

        //public string Filter
        //{
        //    get => this.filter;
        //    set
        //    {
        //        SetValue(ref this.filter, value);
        //        this.Search();
        //    }
        //}

        public OcupationViewModel()
        {
            apiService = new ApiService();

            LoadOcupations();
        }

        public ICommand RefreshCommand => new RelayCommand(LoadOcupations);
        
        // public ICommand SearchCommand => new RelayCommand(Search);

        private async void LoadOcupations()
        {
            this.IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    connection.Message,
                    Languages.BtnAcceptDialog);

                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }

            var response = await apiService.GetList<Ocupation>(
                "https://goodworkers-api.herokuapp.com/",
                "api/",
                "ocupations");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return;
            }

            var ocupationList = (List<Ocupation>)response.Result;
            Ocupations = new ObservableCollection<Ocupation>(ocupationList.OrderBy(o => o.OcupationName));

            this.IsRefreshing = false;

            Application.Current.MainPage = new MasterPage();
        }

        //private void Search()
        //{
        //    if (string.IsNullOrEmpty(this.Filter))
        //    {

        //    }
        //}
    }
}
