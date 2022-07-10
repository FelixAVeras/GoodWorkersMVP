using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ApiService apiservice;
        DialogHelper dialogHelper;

        string _email;
        string _password;
        bool _isToggled;
        bool _isRunning;
        bool _isEnable;

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        public bool IsToggled
        {
            get { return _isToggled; }
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsToggled)));
                }
            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsEnable
        {
            get { return _isEnable; }
            set
            {
                if (_isEnable != value)
                {
                    _isEnable = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnable)));
                }
            }
        }

        //Commands
        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        public LoginViewModel()
        {
            apiservice = new ApiService();

            dialogHelper = new DialogHelper();

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
                    Languages.ErrorDialogTitle,
                    Languages.LoginValidationEmailEmptyMessage,
                    Languages.AcceptDialogButton);

                return;
            }

            if (!RegexUtilities.isValidEmail(Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.LoginValidationEmailInvalidMessage,
                    Languages.AcceptDialogButton);

                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.LoginValidationPasswordEmpty,
                    Languages.AcceptDialogButton);

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
                        Languages.ErrorDialogTitle,
                    connection.Message,
                    Languages.AcceptDialogButton);

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
            await Application.Current.MainPage.Navigation.PushAsync(new OcupationPage());
        }
    }
}
