using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        ApiService apiservice;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        private bool isRunning;
        private bool isEnable;

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

        public RegisterViewModel()
        {
            apiservice = new ApiService();

            IsEnable = true;
        }

        public ICommand RegisterCommand => new RelayCommand(Register);

        async void Register()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    "Debe insertar un Nombre Valido",
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    "Debe insertar un Apellido valido",
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    "Debe insertar una Direccion valida",
                    Languages.BtnAcceptDialog);

                return;
            }

            if (Address.Length <= 50)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    "Su direccion debe de contener mas de 50 caracteres",
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(Cellphone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    "Debe insertar un Numero de Telefono valido",
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

            if (Password.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Contraseña Incorrecta",
                    "La Contraseña debe de ser mayor a 6 caracteres",
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.ErrorPasswordEmptyLabel,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (!Password.Equals(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    "Las Contraseñas no coinciden",
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

            var customer = new User
            {
                UserTypeId = 1,
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                Cellphone = Cellphone,
                Email = Email,
                Password = Password
            };

            //var response = await apiservice.GetToken(
            //    "https://goodworkers-api.herokuapp.com/api/login",
            //    Email, Password);

            //if (!response.IsSuccess || response == null || string.IsNullOrEmpty(response.AccessToken))
            //{
            //    IsRunning = false;
            //    IsEnable = true;

            //    await dialogHelper.ShowMessage("Error", response.ErrorDescription);
            //    Password = null;
            //    return;
            //}

            //var response2 = await apiservice.GetToken(
            //    "https://goodworkers-api.herokuapp.com/api/login",
            //    Email, Password);

            //if (!response2.IsSuccess || response2 == null || string.IsNullOrEmpty(response2.AccessToken))
            //{
            //    IsRunning = false;
            //    IsEnable = true;

            //    await dialogHelper.ShowMessage("Error", response2.ErrorDescription);
            //    Password = null;
            //    return;
            //}

            MainViewModel.GetInstance().Ocupations = new OcupationViewModel();
            Application.Current.MainPage = new MasterPage();

            IsRunning = false;
            IsEnable = true;
        }
    }
}
