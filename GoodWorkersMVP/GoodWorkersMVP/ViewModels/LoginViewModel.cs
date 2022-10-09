using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        ApiService apiservice;

        private string email;
        private string password;
        private bool isRemember;
        private bool isRunning;
        private bool isEnable;

        public string Email
        {
            get => email;
            set => SetValue(ref email, value);
        }

        public string Password
        {
            get => password;
            set => SetValue(ref password, value);
        }

        public bool IsRemember
        {
            get => isRemember;
            set => SetValue(ref isRemember, value);
        }

        public bool IsRunning
        {
            get => isRunning;
            set => SetValue(ref isRunning, value);
        }

        public bool IsEnable
        {
            get => isEnable;
            set => SetValue(ref isEnable, value);
        }

        public ICommand LoginCommand => new RelayCommand(Login);

        // public ICommand RegisterCommand => new RelayCommand(Register);

        public LoginViewModel()
        {
            apiservice = new ApiService();

            IsEnable = true;
            IsRemember = true;

            this.Email = "edlopez23@yopmail.com";
            this.Password = "test1234";
        }

        async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.ErrorEmailEmptyLabel,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (!RegexUtilities.isValidEmail(Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.ErrorEmailInvalidLabel,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.ErrorPasswordEmptyLabel,
                    Languages.BtnAcceptDialog);

                return;
            }

            IsRunning = true;
            IsEnable = false;

            var connection = await apiservice.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnable = true;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    connection.Message,
                    Languages.BtnAcceptDialog);

                return;
            }

            //var response = await apiservice.GetToken(
            //    "https://goodworkers-api.herokuapp.com/api/login",
            //    Email, Password);

            //if (response == null || string.IsNullOrEmpty(response.AccessToken))
            //{
            //    IsRunning = false;
            //    IsEnable = true;

            //    await dialogHelper.ShowMessage("Error", response.ErrorDescription);
            //    Password = null;
            //    return;
            //}

            MainViewModel.GetInstance().Ocupations = new OcupationViewModel();
            Application.Current.MainPage = new MasterPage();

            IsRunning = false;
            IsEnable = true;
        }

        //async void Register()
        //{
        //    MainViewModel.GetInstance().Register = new RegisterViewModel();
        //    await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        //}
    }
}
