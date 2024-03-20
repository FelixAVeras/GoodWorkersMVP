using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models.ModelResponse;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using Newtonsoft.Json;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        readonly ApiService apiService;

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
        public ICommand RegisterCommand => new RelayCommand(Register);

        public LoginViewModel()
        {
            apiService = new ApiService();

            IsEnable = true;
            IsRemember = false;

            Email = "example@example.com";
            Password = "MiPasswordSeguro123";
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

            var connection = await apiService.CheckConnection();

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

            string url = Application.Current.Resources["UrlAPI"].ToString();
            string prefix = Application.Current.Resources["Prefix"].ToString();
            string controller = Application.Current.Resources["loginEndPoint"].ToString();

            string deviceName = DeviceInfo.Name;

            var response = await apiService.GetToken(url, prefix, controller, Email, Password, deviceName);

            if (response == null || string.IsNullOrEmpty(response.Token))
            {
                IsRunning = false;
                IsEnable = true;

                await Application.Current.MainPage.DisplayAlert(
                     Languages.ErrorTitleDialog,
                     Languages.SomethingWentWrong,
                     Languages.BtnAcceptDialog);

                return;
            }

            Settings.IsRemember = IsRemember;
            Settings.AccessToken = response.Token;
            Settings.Token = JsonConvert.SerializeObject(response.Token);

            MainViewModel.GetInstance().Ocupations = new OcupationViewModel();
            Application.Current.MainPage = new MasterPage();

            Email = null;
            Password = null;

            IsRunning = false;
            IsEnable = true;
        }

        async void Register()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());

            // await Launcher.OpenAsync("https://forms.gle/AqoXWD6AJkqFci798");
        }
    }
}