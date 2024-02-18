using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace GoodWorkersMVP.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        ApiService apiService;

        private bool isRefreshing;

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetValue(ref isRefreshing, value);
        }

        private int selectedUserId;

        public int SelectedUserId
        {
            get => selectedUserId;
            set => SetValue(ref selectedUserId, value);
        }

        private User user;

        public User User
        {
            get => user;
            set => SetValue(ref user, value);
        }

        public ProfileViewModel(User selectedUser)
        {
            apiService = new ApiService();

            this.User = selectedUser;

            GetUserData(selectedUser.Id);
        }

        private async void GetUserData(int userId)
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

            var response = await apiService.Get<User>(
                "https://aqueous-beach-68994-3f94c7a633d0.herokuapp.com/",
                "api/",
                "users", userId);

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return;
            }


            this.User = (User)response.Result;

            this.IsRefreshing = false;

            await App.Navigator.PushAsync(new ProfilePage());
        }
    }
}
