using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Services;
using Newtonsoft.Json;
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

        private bool isCustomer;
        private bool isWorker;
        private bool showOcupationPicker;

        private ObservableCollection<DocumentType> documentTypes;
        private DocumentType documentType;

        private ObservableCollection<Ocupation> ocupations;
        private Ocupation ocupation;

        public ObservableCollection<string> UserTypes { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
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

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                "Tomar Imagen Desde: ",
                "Cancelar",
                null,
                "Desde la Galeria",
                "Desde la Camara");

            if (source == "Cancelar")
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
