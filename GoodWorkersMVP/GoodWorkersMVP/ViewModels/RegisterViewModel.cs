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
using Xamarin.Essentials;
using Xamarin.Forms;

using GoodWorkersMVP.Models.ModelResponse;

namespace GoodWorkersMVP.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private ApiService apiService;

        private MediaFile file;
        private ImageSource imageSource;

        private bool isRunning;
        private bool isEnabled;
        private bool isEntryVisible;

        private bool showOcupationPicker;

        private ObservableCollection<UserType> userTypes;
        private UserType userType;

        private ObservableCollection<DocumentType> documentTypes;
        private DocumentType documentType;

        private ObservableCollection<Ocupation> ocupations;
        private Ocupation ocupation;

        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Cellphone { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int DocumentTypeId { get; set; }
        public int UserTypeId { get; set; }
        public int OcupationID { get; set; }
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

        // Especifique field
        public string EspecifyOcupation { get; set; } = string.Empty;

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

        public bool IsEntryVisible
        {
            get => isEntryVisible;
            set
            {
                if (isEntryVisible != value)
                {
                    isEntryVisible = value;
                    OnPropertyChanged(nameof(IsEntryVisible));
                }
            }
        }

        public ImageSource ImageSource
        {
            get => imageSource;
            set => SetValue(ref imageSource, value);
        }

        private UserType selectedUserType;
        public UserType SelectedUserType
        {
            get => selectedUserType;
            set
            {
                if (selectedUserType != value)
                {
                    selectedUserType = value;
                    OnPropertyChanged(nameof(SelectedUserType));
                    ChangeUserType();
                }
            }
        }

        private Ocupation selectedOcupacion;
        public Ocupation SelectedOcupacion
        {
            get => selectedOcupacion;
            set
            {
                if (selectedOcupacion != value)
                {
                    selectedOcupacion = value;
                    OnPropertyChanged(nameof(SelectedOcupacion));

                    IsEntryVisible = value?.OcupationName == "Otros";
                }
            }
        }

        private DocumentType selectedDocumentTypes;
        public DocumentType SelectedDocumentTypes
        {
            get => selectedDocumentTypes;
            set
            {
                if (selectedDocumentTypes != value)
                {
                    selectedDocumentTypes = value;
                    OnPropertyChanged(nameof(selectedDocumentTypes));
                }
            }
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

        public ObservableCollection<DocumentType> DocumentTypes
        {
            get => documentTypes;
            set => SetValue(ref documentTypes, value);
        }

        public ObservableCollection<UserType> UserTypes
        {
            get => userTypes;
            set => SetValue(ref userTypes, value);
        }

        public RegisterViewModel()
        {
            apiService = new ApiService();
            IsEnabled = true;
            ImageSource = "nouser";

            LoadUserTypesFromApi();
            LoadDocumentTypesFromApi();
        }

        public ICommand ChangeProfileImage => new RelayCommand(ChangeImage);
        public ICommand SaveUserCommand => new RelayCommand(SaveUser);

        async void SaveUser()
        {
            if (UserTypeId == 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.UserTypeEmptyMessage,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.FirstNameValidationEmpty,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.LastNameValidationEmpty,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(Phone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.CellphoneValidationEmpty,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(DocumentNumber))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.DocumentNumberValidationEmpty,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.AddressValidationEmpty,
                    Languages.BtnAcceptDialog);

                return;
            }

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
                    Languages.PasswordLengthMessage,
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
                    Languages.PasswordValidationConfirm,
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

            if (file != null)
            {
                imageArray = FileHelper.ReadFull(this.file.GetStream());
            }

            var userRequest = new UserRequest
            {
                AboutMe = AboutMe,
                Address = Address,
                Birthday = Birthday,
                Cellphone = Cellphone,
                DocumentTypeId = SelectedDocumentTypes?.DocumentTypeID ?? 0,
                DocumentNumber = DocumentNumber,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                MiddleName = MiddleName,
                OcupationID = SelectedOcupacion?.Id ?? 0,
                ImageArray = imageArray,
                Password = Password,
                UserName = Email,
                DeviceName = DeviceInfo.Name
            };

            string url = Application.Current.Resources["UrlAPI"].ToString();
            string prefix = Application.Current.Resources["Prefix"].ToString();
            string controller = Application.Current.Resources["registerEndPoint"].ToString();
            
            if (selectedOcupacion.OcupationName == "Otros")
            {
                if (string.IsNullOrEmpty(EspecifyOcupation))
                {
                    IsRunning = false;
                    IsEnabled = true;

                    await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.OcupationEmptyMessage,
                    Languages.BtnAcceptDialog);

                    return;
                }

                Response responseOcupation = await apiService.Post(url, prefix, "ocupations", EspecifyOcupation);

                if (!responseOcupation.IsSuccess) 
                {
                    IsRunning = false;
                    IsEnabled = true;

                    await Application.Current.MainPage.DisplayAlert(
                        Languages.ErrorTitleDialog,
                        responseOcupation.Message,
                        Languages.BtnAcceptDialog);

                    return;
                }
            }

            Response response = await apiService.Post(url, prefix, controller, userRequest);

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
                    Languages.RegisterConfirmationTitleDialog,
                    Languages.RegisterConfirmationMessage,
                    Languages.BtnAcceptDialog);

            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.TakeImgFrom,
                Languages.BtnCancelDialog,
                null,
                Languages.FromCamera,
                Languages.FromGallery);

            if (source == Languages.BtnCancelDialog)
            {
                file = null;
                return;
            }

            if (source == Languages.FromCamera)
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
            if (SelectedUserType != null)
            {
                if (SelectedUserType.UserTypeName == "Cliente")
                {
                    ShowOcupationPicker = false;

                    UserTypeId = 1;
                }
                else
                {
                    ShowOcupationPicker = true;

                    UserTypeId = selectedUserType.UserTypeId;

                    LoadOcupacionesFromApi();
                }
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

        private async void LoadUserTypesFromApi()
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

            var response = await apiService.GetList<UserType>(
                "https://aqueous-beach-68994-3f94c7a633d0.herokuapp.com/",
                "api/",
                "userTypes");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return;
            }

            List<UserType> userTypesList = (List<UserType>)response.Result;
            UserTypes = new ObservableCollection<UserType>(userTypesList);
        }

        private async void LoadDocumentTypesFromApi()
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

            var response = await apiService.GetList<DocumentType>(
                "https://aqueous-beach-68994-3f94c7a633d0.herokuapp.com/",
                "api/",
                "documentTypes");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    response.Message,
                    Languages.BtnAcceptDialog);

                return;
            }

            List<DocumentType> documentTypesList = (List<DocumentType>)response.Result;
            DocumentTypes = new ObservableCollection<DocumentType>(documentTypesList);
        }
    }
}
