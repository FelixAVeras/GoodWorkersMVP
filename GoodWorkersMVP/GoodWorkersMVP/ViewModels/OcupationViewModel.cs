using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class OcupationViewModel : BaseViewModel
    {
        ApiService apiService;

        private bool isRefreshing;
        private string filter;
        private ObservableCollection<Ocupation> ocupations;
        private List<Ocupation> ocupationsList;

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
            set { SetValue(ref filter, value); this.Search(); }
        }

        public OcupationViewModel()
        {
            apiService = new ApiService();

            LoadOcupations();
        }

        public ICommand SearchCommand => new RelayCommand(Search);
        public ICommand RefreshCommand => new RelayCommand(LoadOcupations);

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
                "https://aqueous-beach-68994-3f94c7a633d0.herokuapp.com/",
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

            this.ocupationsList = (List<Ocupation>)response.Result;
            this.Ocupations = new ObservableCollection<Ocupation>(ocupationsList.OrderBy(o => o.OcupationName));

            this.IsRefreshing = false;

            Application.Current.MainPage = new MasterPage();
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Ocupations = new ObservableCollection<Ocupation>(this.ocupationsList);
            }
            else
            {
                this.Ocupations = new ObservableCollection<Ocupation>(
                    this.ocupationsList.Where(o => o.OcupationName.ToLower()
                                                              .Contains(this.filter.ToLower())));
            }
        }
    }
}
