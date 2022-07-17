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
        NavigationService navigationService;

        string _email;
        string _password;
        bool _isToggled;
        bool _isRunning;
        bool _isEnable;

        public string Email
        {
            get { return _email; }
            set { SetValue(ref _email, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetValue(ref _password, value); }
        }

        public bool IsToggled
        {
            get { return _isToggled; }
            set { SetValue(ref _isToggled, value); }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetValue(ref _isRunning, value); }
        }

        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetValue(ref _isEnable, value); }
        }

        //Commands
        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        public ICommand RegisterCommand
        {
            get { return new RelayCommand(Register); }
        }

        public LoginViewModel()
        {
            apiservice = new ApiService();
            navigationService = new NavigationService();

            IsEnable = true;
            IsToggled = true;

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
            //await navigationService.NavigateOnMaster("OcupationPage"); 
            //navigationService.SetMainPage("MasterPage");

            IsRunning = false;
            IsEnable = true;
        }

        async void Register()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            navigationService.NavigateOnLogin("RegisterPage");
        }
    }
}
