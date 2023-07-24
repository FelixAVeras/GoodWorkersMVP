using GoodWorkersMVP.Models;
using GoodWorkersMVP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GoodWorkersMVP.Helpers;
using Xamarin.Forms;
using System.Linq;
using GoodWorkersMVP.ViewModels.ItemViewModel;

namespace GoodWorkersMVP.ViewModels
{
    public class OcupationUserViewModel : BaseViewModel
    {
        //private ItemViewModel.OcupationItemViewModel ocupationItemViewModel;

        //ApiService apiService = new ApiService();

        //private bool isRefreshing;
        ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get => _users;
            set => SetValue(ref _users, value);
        }

        //public bool IsRefreshing
        //{
        //    get => isRefreshing;
        //    set => SetValue(ref isRefreshing, value);
        //}

        //public OcupationUserViewModel()
        //{
        //    //Users = new ObservableCollection<User>();
        //}

        //public Ocupation Ocupation { get; set; }
        List<User> users;
        OcupationItemViewModel ocupationItemViewModel;

        //public OcupationUserViewModel(/*Ocupation ocupationItemViewModel*/ List<User> users)
        //{
        //    //this.ocupationItemViewModel = ocupationItemViewModel;
        //    //this.Ocupation = ocupationItemViewModel;

        //    this.users = users;
        //}

        public OcupationUserViewModel(OcupationItemViewModel ocupationItemViewModel)
        {
            this.ocupationItemViewModel = ocupationItemViewModel;

            Users = new ObservableCollection<User>(users.OrderBy(u => u.FirstName));
        }

        //public async Task LoadUserByOcupationId(int ocupationId)
        //{
        //    IsRefreshing = true;

        //    var connection = await apiService.CheckConnection();

        //    if (!connection.IsSuccess)
        //    {
        //        IsRefreshing = false;

        //        await Application.Current.MainPage.DisplayAlert(
        //            Languages.ErrorTitleDialog,
        //            connection.Message,
        //            Languages.BtnAcceptDialog);

        //        await Application.Current.MainPage.Navigation.PopAsync();

        //        return;
        //    }

        //    var response = await apiService.GetList<User>(
        //        "https://aqueous-beach-68994-3f94c7a633d0.herokuapp.com/",
        //        "api/",
        //        "ocupations/",
        //        ocupationId);

        //    if (!response.IsSuccess)
        //    {
        //        await Application.Current.MainPage.DisplayAlert(
        //            Languages.ErrorTitleDialog,
        //            response.Message, 
        //            Languages.BtnAcceptDialog);

        //        return;
        //    }

        //    var userList = (List<User>)response.Result;
        //    Users = new ObservableCollection<User>(userList);

        //    isRefreshing = false;
    }
}

