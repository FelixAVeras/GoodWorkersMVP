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
        ObservableCollection<Ocupation> _ocupations;
        private string filter;

        public ObservableCollection<Ocupation> OcupationsList
        {
            get { return _ocupations; }
            set { SetValue(ref _ocupations, value); }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetValue(ref isRefreshing, value); }
        }

        //public string Filter
        //{
        //    get => filter;
        //    set
        //    {
        //        SetValue(ref filter, value);
        //        Search();
        //    }
        //}

        public OcupationViewModel()
        {
            apiService = new ApiService();

            LoadOcupations();
        }

        public ICommand RefreshCommand
        {
            get { return new RelayCommand(LoadOcupations); }
        }

        //public ICommand SearchCommand
        //{
        //    get { return new RelayCommand(Search); }
        //}

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
            OcupationsList = new ObservableCollection<Ocupation>(ocupationList.OrderBy(o => o.OcupationName));

            Application.Current.MainPage = new MasterPage();

            IsRefreshing = false;
        }

        //private IEnumerable<OcupationItemViewModel> ToCategoryItemViewModel()
        //{
        //    return MainViewModel.GetInstance().Ocupations.Select(cl => new OcupationItemViewModel
        //    {
        //        Id = cl.Id,
        //        OcupationName = cl.OcupationName
        //    });
        //}

        //private void Search()   
        //{
        //    if (string.IsNullOrEmpty(Filter))
        //    {
        //        CategoriesList = new ObservableCollection<CategoryItemViewModel>(ToCategoryItemViewModel());
        //    }
        //    else
        //    {
        //        CategoriesList = new ObservableCollection<CategoryItemViewModel>(ToCategoryItemViewModel()
        //            .Where(cl => cl.OcupationName.ToLower().Contains(this.Filter.ToLower())));
        //    }
        //}
    }
}
