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
        private ObservableCollection<Ocupation> ocupations;
        private string filter;

        private List<Ocupation> OcupationList;

        public ObservableCollection<Ocupation> Ocupations
        {
            get { return ocupations; }
            set { SetValue(ref ocupations, value); }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetValue(ref isRefreshing, value); }
        }

        public string Filter
        {
            get => filter;
            set
            {
                SetValue(ref filter, value);
                this.Search();
            }
        }

        public OcupationViewModel()
        {
            apiService = new ApiService();

            LoadOcupations();
        }

        public ICommand RefreshCommand => new RelayCommand(LoadOcupations);
        public ICommand SearchCommand => new RelayCommand(Search);

        async void LoadOcupations()
        {
            //this.IsRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                //this.IsRefreshing = false;

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
                //this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return;
            }

            this.OcupationList = (List<Ocupation>)response.Result;
            Ocupations = new ObservableCollection<Ocupation>(OcupationList.OrderBy(o => o.OcupationName));

            Application.Current.MainPage = new MasterPage();

            //this.IsRefreshing = false;
        }

        //private IEnumerable<OcupationItemViewModel> ToCategoryItemViewModel()
        //{
        //    return MainViewModel.GetInstance().Ocupations.Select(cl => new OcupationItemViewModel
        //    {
        //        Id = cl.Id,
        //        OcupationName = cl.OcupationName
        //    });
        //}

        private void Search()   
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Ocupations = new ObservableCollection<Ocupation>(this.OcupationList);
            }
            else
            {
                this.Ocupations = new ObservableCollection<Ocupation>(
                    this.OcupationList.Where(ol => ol.OcupationName.ToLower().Contains(this.Filter.ToLower())));
            }
        }
    }
}
