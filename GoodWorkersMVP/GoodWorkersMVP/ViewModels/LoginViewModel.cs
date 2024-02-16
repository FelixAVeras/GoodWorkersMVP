using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        ApiService apiService;

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

        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged(nameof(IsExpanded));
                }
            }
        }

        public ICommand ToggleExpandCommand => new Command(() => IsExpanded = !IsExpanded);
        public ICommand LoginCommand => new RelayCommand(Login);
        public ICommand RegisterCommand => new RelayCommand(Register);

        public LoginViewModel()
        {
            apiService = new ApiService();

            IsEnable = true;
            IsRemember = true;
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

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["Prefix"].ToString();
            var controller = Application.Current.Resources["loginEndPoint"].ToString();

            var response = await apiService.GetToken(url, prefix, controller, Email, Password);

            if (response == null || string.IsNullOrEmpty(response.AccessToken))
            {
                IsRunning = false;
                IsEnable = true;

                await Application.Current.MainPage.DisplayAlert(
                     Languages.ErrorTitleDialog,
                     Languages.SomethingWentWrong,
                     Languages.BtnAcceptDialog);

                Email = null;
                Password = null;

                return;
            }

            Settings.TokenType = response.TokenType;
            Settings.AccessToken = response.AccessToken;
            Settings.IsRemember = IsRemember;

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
        }
    }
}
