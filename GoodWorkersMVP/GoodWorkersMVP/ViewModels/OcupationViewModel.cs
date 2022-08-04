using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;

namespace GoodWorkersMVP.ViewModels
{
    public class OcupationViewModel : BaseViewModel
    {
        ApiService apiService;

        private bool isRefreshing;
        private ObservableCollection<Ocupation> ocupations;
        private string filter;

        //private List<Ocupation> OcupationList;

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
            
            MainViewModel.GetInstance().OcupationList = (List<Ocupation>)response.Result;
            Ocupations = new ObservableCollection<Ocupation>(this.ToOcupationItemViewModel());

            Application.Current.MainPage = new MasterPage();

            //this.IsRefreshing = false;
        }

        private IEnumerable<Ocupation> ToOcupationItemViewModel()
        {
            return MainViewModel.GetInstance().OcupationList.Select(ol => new OcupationItemViewModel
            {
                Id = ol.Id,
                OcupationName = ol.OcupationName
            }).ToList();
        }

        private void Search()   
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                Ocupations = new ObservableCollection<Ocupation>(this.ToOcupationItemViewModel());
            }
            else
            {
                this.Ocupations = new ObservableCollection<Ocupation>(
                    this.ToOcupationItemViewModel()
                        .Where(ol => ol.OcupationName.ToLower()
                        .Contains(this.Filter.ToLower())));
            }
        }
    }
}
