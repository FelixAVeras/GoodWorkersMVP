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

        private bool isRefreshing;

        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("cellphone")]
        public string Cellphone { get; set; }

        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }

        [JsonProperty("document_number")]
        public string DocumentNumber { get; set; }

        [JsonProperty("about_me")]
        public string AboutMe { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("password_confirmation")]
        public string PasswordConfirmation { get; set; }

        [JsonProperty("document_type_id")]
        public long DocumentTypeId { get; set; }

        [JsonProperty("userType_id")]
        public long UserTypeId { get; set; }
        public UserType UserType { get; set; }

        [JsonProperty("ocupation_id")]
        public int OcupationID { get; set; }
        public Ocupation Ocupation { get; set; }

        [JsonProperty("")]
        public byte[] ImageArray { get; set; }

        [JsonProperty("device_name")]
        public string DeviceName { get; set; }

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
            IsRefreshing = true;

            string url = Application.Current.Resources["UrlAPI"].ToString();
            string prefix = Application.Current.Resources["Prefix"].ToString();
            string controller = Application.Current.Resources["userEndPoint"].ToString();

            var response = await apiService.Get<User>(url, prefix, controller, userId, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return;
            }


            User = (User)response.Result;

            IsRefreshing = false;

            await App.Navigator.PushAsync(new ProfilePage());
        }
    }
}
