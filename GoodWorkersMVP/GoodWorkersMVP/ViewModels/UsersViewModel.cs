using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using GoodWorkersMVP.ViewModels.ItemViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        ApiService apiService;

        private bool isRefreshing;
        private string filter;
        //private List<User> users;
        private List<User> usersList;

        //public List<User> Users
        public User User { get; set; }
        public Ocupation ocupation;
        //{
        //    get => users;
        //    set
        //    {
        //        users = value;
        //        SetValue(ref users, value);
        //    }
        //}

        public List<User> Users { get; set; }

        //public ObservableCollection<User> Users
        //{
        //    get => users;
        //    set => SetValue(ref users, value);
        //}

        //public bool IsRefreshing
        //{
        //    get => isRefreshing;
        //    set => SetValue(ref isRefreshing, value);
        //}

        public string Filter
        {
            get => filter;
            set { SetValue(ref filter, value); /*RefreshList();*/ }
        }

        //public UsersViewModel(List<User> users)
        //{
        //    Users = users;
        //}
        //private OcupationItemViewModel ocupationItemViewModel;

        //public UsersViewModel(OcupationItemViewModel ocupationItemViewModel)
        //{
        //    this.ocupationItemViewModel = ocupationItemViewModel;
        //}

        public UsersViewModel(User user)
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadUsers();
            User = user;
        }

        private static UsersViewModel instance;

        public static UsersViewModel Instance()
        {
            return instance;
        }



        private async void LoadUsers()
        {
            this.isRefreshing = true;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                isRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    connection.Message,
                    Languages.BtnAcceptDialog);

                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }

            var response = await this.LoadUsersFromAPI();

            if (response)
            {
                //this.RefreshList();
                await Application.Current.MainPage.DisplayAlert(
                    "Informacion",
                    response.ToString(),
                    Languages.BtnAcceptDialog);
            }

            isRefreshing = false;
        }

        private async Task<bool> LoadUsersFromAPI() 
        {
            var urlApi = Application.Current.Resources["UrlApi"].ToString();
            var urlPrefix = Application.Current.Resources["urlPrefix"].ToString();
            var urlController = Application.Current.Resources["UrlUsers"].ToString();

            var response = await apiService.GetList<Ocupation>(urlApi, urlPrefix, urlController, this.ocupation.Id);

            if (!response.IsSuccess)
            {
                isRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return false;
            }

            this.Users = (List<User>)response.Result;
            return true;
        }
    }
}

