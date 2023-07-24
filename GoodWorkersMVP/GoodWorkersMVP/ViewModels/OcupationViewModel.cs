using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using GoodWorkersMVP.ViewModels.ItemViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class OcupationViewModel : BaseViewModel
    {
        ApiService apiService;

        private bool isRefreshing;
        private string filter;
        //private ObservableCollection<Ocupation> ocupations;
        private ObservableCollection<OcupationItemViewModel> ocupations;
        private List<Ocupation> ocupationsList;

        //public ObservableCollection<Ocupation> Ocupations
        public ObservableCollection<OcupationItemViewModel> Ocupations
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
            set { SetValue(ref filter, value); RefreshList(); } 
        }

        public OcupationViewModel()
        {
            apiService = new ApiService();

            LoadOcupations();
        }

        public ICommand SearchCommand => new RelayCommand(RefreshList);
        public ICommand RefreshCommand => new RelayCommand(async () => await LoadOcupations());

        private async Task LoadOcupations()
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

            var urlApi = Application.Current.Resources["UrlApi"].ToString();
            var urlPrefix = Application.Current.Resources["urlPrefix"].ToString();
            var urlController = Application.Current.Resources["UrlOcupations"].ToString();

            var response = await apiService.GetList<Ocupation>(urlApi, urlPrefix, urlController);

            if (!response.IsSuccess)
            {
                IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return;
            }

            ocupationsList = (List<Ocupation>)response.Result;
            //Ocupations = new ObservableCollection<Ocupation>(ocupationsList);
            //Ocupations = new ObservableCollection<OcupationItemViewModel>(this.ToOcupationItemViewModel());

            this.RefreshList();

            IsRefreshing = false;

            Application.Current.MainPage = new MasterPage();
        }

        //private void Search()
        //{
        //    if (string.IsNullOrEmpty(this.Filter))
        //    {
        //        //Ocupations = new ObservableCollection<Ocupation>(ocupationsList);
        //        Ocupations = new ObservableCollection<OcupationItemViewModel>(this.ToOcupationItemViewModel());
        //    }
        //    else
        //    {
        //        //Ocupations = new ObservableCollection<Ocupation>(
        //        //    ocupationsList.Where(o => o.OcupationName.ToLower().Contains(filter.ToLower())));

        //        Ocupations = new ObservableCollection<OcupationItemViewModel>(
        //            ToOcupationItemViewModel()
        //                .Where(o => o.OcupationName.ToLower().Contains(filter.ToLower())));
        //    }
        //}

        private IEnumerable<OcupationItemViewModel> ToOcupationItemViewModel()
        {
            return this.ocupationsList.Select(ol => new OcupationItemViewModel
            {
                OcupationName = ol.OcupationName,
                Users = ol.Users,
            }).OrderBy(ol => ol.OcupationName);
        }

        private void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                var myOcupationsListItemViewModel = this.Ocupations.Select(o => new OcupationItemViewModel
                {
                    Id = o.Id,
                    OcupationName = o.OcupationName
                });

                this.Ocupations = new ObservableCollection<OcupationItemViewModel>(
                    myOcupationsListItemViewModel.OrderBy(o => o.OcupationName));
            }
            else
            {
                var myOcupationsListItemViewModel = this.Ocupations.Select(o => new OcupationItemViewModel
                {
                    Id = o.Id,
                    OcupationName = o.OcupationName
                }).Where(o => o.OcupationName.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.Ocupations = new ObservableCollection<OcupationItemViewModel>(
                    myOcupationsListItemViewModel.OrderBy(o => o.OcupationName));
            }
        }
    }
}
