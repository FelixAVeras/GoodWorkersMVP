using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private ApiService apiService;

        private MediaFile file;
        private ImageSource imageSource;

        private bool isRunning;
        private bool isEnabled;

        private bool showOcupationPicker;

        private ObservableCollection<DocumentType> documentTypes;
        private DocumentType documentType;

        private ObservableCollection<Ocupation> ocupations;
        private Ocupation ocupation;

        public ObservableCollection<string> UserTypes { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Cellphone { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
        public DateTimeOffset Birthday { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int DocumentTypeId { get; set; }
        public int UserTypeId { get; set; }
        public int OcupationID { get; set; }
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

        public bool IsRunning
        {
            get => isRunning;
            set => SetValue(ref isRunning, value);
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set => SetValue(ref isEnabled, value);
        }

        public ImageSource ImageSource
        {
            get => imageSource;
            set => SetValue(ref imageSource, value);
        }

        private string selectedUserType;
        public string SelectedUserType
        {
            get => selectedUserType;
            set
            {
                if (selectedUserType != value)
                {
                    selectedUserType = value;
                    OnPropertyChanged(nameof(SelectedUserType));
                    ChangeUserTypeCommand.Execute(null);
                }
            }
        }

        private string selectedOcupacion;
        public string SelectedOcupacion
        {
            get => selectedOcupacion;
            set => SetValue(ref selectedOcupacion, value);
        }

        public bool ShowOcupationPicker
        {
            get => showOcupationPicker;
            set => SetValue(ref showOcupationPicker, value);
        }

        public ObservableCollection<Ocupation> Ocupations
        {
            get => ocupations;
            set => SetValue(ref ocupations, value);
        }

        public Ocupation Ocupation
        {
            get => ocupation;
            set => SetValue(ref ocupation, value);
        }

        public RegisterViewModel()
        {
            apiService = new ApiService();
            IsEnabled = true;
            ImageSource = "nouser";
        }

        public ICommand ChangeProfileImage => new RelayCommand(ChangeImage);
        public ICommand ChangeUserTypeCommand => new RelayCommand(ChangeUserType);
        public ICommand SaveUserCommand => new RelayCommand(SaveUser);

        async void SaveUser()
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

            if (this.Password.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    "La contraseña debe de ser mayor a 6 caracteres",
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

            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.ErrorPasswordEmptyLabel,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (!this.Password.Equals(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    "Las Contraseñas no coinciden",
                    Languages.BtnAcceptDialog);

                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    connection.Message,
                    Languages.BtnAcceptDialog);

                return;
            }

            byte[] imageArray = null;

            if (this.file != null)
            {
                imageArray = FileHelper.ReadFull(this.file.GetStream());
            }

            var userRequest = new UserRequest
            {
                AboutMe = this.AboutMe,
                Address = this.Address,
                Birthday = this.Birthday,
                Cellphone = this.Cellphone,
                DocumentTypeId = 1,
                DocumentNumber = this.DocumentNumber,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                MiddleName = this.MiddleName,
                OcupationId = 1,
                ImageArray = imageArray,
                Password = this.Password,
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["Prefix"].ToString();
            var controller = Application.Current.Resources["RegisterEndPoint"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, userRequest);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return;
            }

            this.isRunning = false;
            this.isEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                    "Exito!",
                    "Usuario Creado Exitosamente, ahora puede ingresar con su usuario y contraseña.",
                    Languages.BtnAcceptDialog);

            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                "Tomar Imagen Desde: ",
                Languages.BtnCancelDialog,
                null,
                "Desde la Galeria",
                "Desde la Camara");

            if (source == Languages.BtnCancelDialog)
            {
                file = null;
                return;
            }

            if (source == "Desde la Camara")
            {
                file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    });
            }
            else
            {
                file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() => file.GetStream());
            }
        }

        private void ChangeUserType()
        {
            if (SelectedUserType == "Cliente")
            {
                ShowOcupationPicker = false;
            }
            else
            {
                ShowOcupationPicker = true;

                LoadOcupacionesFromApi();
            }
        }

        private async void LoadOcupacionesFromApi()
        {
            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    connection.Message,
                    Languages.BtnAcceptDialog);

                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }

            var response = await apiService.GetList<Ocupation>(
                "https://aqueous-beach-68994-3f94c7a633d0.herokuapp.com/",
                "api/",
                "ocupations");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return;
            }

            List<Ocupation> ocupacionesList = (List<Ocupation>)response.Result;
            Ocupations = new ObservableCollection<Ocupation>(ocupacionesList);
        }
    }
}
