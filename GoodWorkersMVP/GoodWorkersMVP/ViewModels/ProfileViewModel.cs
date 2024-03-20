using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        ApiService apiService;

        private User user;

        public User User
        {
            get => user;
            set => SetValue(ref user, value);
        }

        public ProfileViewModel(User selectedUser)
        {
            apiService = new ApiService();

            User = selectedUser;

            GetUserData(selectedUser.Id);
        }

        private async void GetUserData(int userId)
        {
            string url = Application.Current.Resources["UrlAPI"].ToString();
            string prefix = Application.Current.Resources["Prefix"].ToString();
            string controller = Application.Current.Resources["userEndPoint"].ToString();

            var response = await apiService.Get<User>(url, prefix, controller, userId, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return;
            }


            User = (User)response.Result;
        }
    }
}
