using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
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

        public bool IsCustomer
        {
            get => isCustomer;
            set
            {
                if (isCustomer != value)
                {
                    isCustomer = value;
                    OnPropertyChanged();
                    ChangeUserTypeCommand.Execute(null);
                }
            }
        }

        public bool IsWorker
        {
            get => isWorker;
            set
            {
                if (isWorker != value)
                {
                    isWorker = value;
                    OnPropertyChanged();
                    ChangeUserTypeCommand.Execute(null); // Ejecutar el comando cuando cambie IsCustomer
                }
            }
        }

        public bool ShowOcupationPicker
        {
            get => showOcupationPicker;
            set => SetValue(ref showOcupationPicker, value);
        }

        //public ObservableCollection<DocumentType> DocumentTypes
        //{
        //    get => documentTypes;
        //    set => SetValue(ref documentTypes, value);
        //}

        //public DocumentType DocumentType
        //{
        //    get => documentType;
        //    set => SetValue(ref documentType, value);
        //}

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

            // LoadDocumentTypesAsync();
        }

        public ICommand ChangeProfileImage => new RelayCommand(ChangeImage);
        public ICommand PickerOcupationSelectedCommand => new RelayCommand(PickerOcupation);
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

        //private async void LoadDocumentTypesAsync()
        //{
        //    var response = await apiService.GetList<User>(
        //        "https://aqueous-beach-68994-3f94c7a633d0.herokuapp.com/",
        //        "api/",
        //        "document_types");

        //    if (!response.IsSuccess)
        //    {
        //        await App.Current.MainPage.DisplayAlert("Error", "No se obtuvieron registros", "Cerrar");
        //        return;
        //    }

        //    var documentTypesList = (List<DocumentType>)response.Result;
        //    DocumentTypes = new ObservableCollection<DocumentType>(documentTypesList);
        //}

        private async void PickerOcupation()
        {
            var response = await apiService.GetList<Ocupation>(
                "https://aqueous-beach-68994-3f94c7a633d0.herokuapp.com/",
                "api/",
                "ocupations");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se obtuvieron registros", "Cerrar");
                return;
            }

            var ocupationList = (List<Ocupation>)response.Result;
            Ocupations = new ObservableCollection<Ocupation>(ocupationList);
        }

        private void ChangeUserType()
        {
            if (IsCustomer)
            {
                ShowOcupationPicker = false;
            }
            else if (IsWorker)
            {
                ShowOcupationPicker = true;
            }
        }
    }
}
