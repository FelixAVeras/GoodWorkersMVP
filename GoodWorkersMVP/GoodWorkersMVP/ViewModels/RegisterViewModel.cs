using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ApiService apiService;

        NavigationService navigationService;

        private bool _isRunning;
        private bool _isEnable;

        //private ImageSource imageSource;
        //private MediaFile file;

        //public ImageSource ImageSource
        //{
        //    get => this.imageSource;
        //    set => SetValue(ref this.imageSource, value);
        //}

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

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Cellphone { get; set; }
        public DateTime Birthday { get; set; }
        //public virtual DocumentType DocumentType { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentNumber { get; set; }
        //public virtual Ocupation Ocupation { get; set; }
        public int OcupationID { get; set; }
        public string AboutMe { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public bool TermAndCondition { get; set; }

        //static HttpClient client;
        public RegisterViewModel()
        {
            this.apiService = new ApiService();
            this.navigationService = new NavigationService();

            this._isEnable = true;
            //this.imageSource = "camera";

            // LoadDocumentTypes();
        }

        //public ICommand ChangeUserPhotoCommand
        //{
        //    get { return new RelayCommand(ChangePhoto); }
        //}

        //private async void ChangePhoto()
        //{
        //    await CrossMedia.Current.Initialize();

        //    if (CrossMedia.Current.IsCameraAvailable &&
        //        CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        var source = await Application.Current.MainPage.DisplayActionSheet(
        //            Languages.TakePickFrom,
        //            Languages.CancelDialogButton,
        //            null,
        //            Languages.TakePicDialogGallery,
        //            Languages.TakePicDialogCamera);

        //        if (source == Languages.CancelDialogButton)
        //        {
        //            this.file = null;
        //            return;
        //        }

        //        if (source == Languages.TakePicDialogCamera)
        //        {
        //            this.file = await CrossMedia.Current.TakePhotoAsync(
        //                new StoreCameraMediaOptions
        //                {
        //                    Directory = "Sample",
        //                    Name = "test.jpg",
        //                    PhotoSize = PhotoSize.Small,
        //                }
        //            );
        //        }
        //        else
        //        {
        //            this.file = await CrossMedia.Current.PickPhotoAsync();
        //        }
        //    }
        //    else
        //    {
        //        this.file = await CrossMedia.Current.PickPhotoAsync();
        //    }

        //    if (this.file != null)
        //    {
        //        this.ImageSource = ImageSource.FromStream(() =>
        //        {
        //            var stream = file.GetStream();
        //            return stream;
        //        });
        //    }
        //}

        public ICommand RegisterCommand => new RelayCommand(Register);

        private async void Register()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.FirstNameValidationEmpty,
                    Languages.AcceptDialogButton);

                return;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.LastNameValidationEmpty,
                    Languages.AcceptDialogButton);

                return;
            }

            if (string.IsNullOrEmpty(Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.AddressValidationEmpty,
                    Languages.AcceptDialogButton);

                return;
            }

            if (string.IsNullOrEmpty(Cellphone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.CellphoneValidationEmpty,
                    Languages.AcceptDialogButton);

                return;
            }

            //TODO: Validacion de la fecha vacia y para mayoria de edad

            if (string.IsNullOrEmpty(DocumentNumber))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.DocumentNumberValidationEmpty,
                    Languages.AcceptDialogButton);

                return;
            }

            if (string.IsNullOrEmpty(AboutMe))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.AboutMeValidationEmpty,
                    Languages.AcceptDialogButton);

                return;
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.LoginValidationEmailEmptyMessage,
                    Languages.AcceptDialogButton);

                return;
            }

            if (!RegexUtilities.isValidEmail(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.EmailValidationCorrect,
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

            if (this.Password.Length < 6 && this.Password.Length > 20)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.PasswordValidationCorrect,
                    Languages.AcceptDialogButton);

                return;
            }

            if (this.Password != this.PasswordConfirm)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    Languages.PasswordValidationConfirm,
                    Languages.AcceptDialogButton);
                return;
            }

            this.IsRunning = true;
            this.IsEnable = false;

            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnable = true;

                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorDialogTitle,
                    checkConnetion.Message,
                    Languages.AcceptDialogButton);
                return;
            }

            //byte[] imageArray = null;
            //if (this.file != null)
            //{
            //    imageArray = FileHelper.ReadFull(this.file.GetStream());
            //}

            var user = new User
            {
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                //ImageArray = imageArray,
                //UserTypeID = 2,
                //Password = this.Password,
                AboutMe = this.AboutMe,
                Address = this.Address,
                Birthday = this.Birthday,
                Cellphone = this.Cellphone,
                //DocumentTypeID = this.DocumentTypeID,
                //DocumentNumber = this.DocumentNumber,
                //OcupationID = this.OcupationID
            };

            var apiurl = Application.Current.Resources["UrlAPI"].ToString();


            this.IsRunning = false;
            this.IsEnable = true;

            await Application.Current.MainPage.DisplayAlert(
                Languages.RegisterConfirmationTitleDialog,
                Languages.RegisterConfirmationMessage,
                Languages.AcceptDialogButton);

            //await Application.Current.MainPage.Navigation.PopAsync();
            await navigationService.BackOnLogin();
            navigationService.SetMainPage("MasterPage");
        }

        //public void OnNavigatedTo(INavigationParameters parameters)
        //{
        //    base.OnNavigatedTo(parameters);

        //    LoadDocumentTypes();
        //}

        //private async void LoadDocumentTypes()
        //{
        //    var url = Application.Current.Resources["UrlAPI"].ToString();
        //    var response = await apiService.GetList<DocumentType>(url, "/api", "/documents");

        //    if (!response.IsSuccess)
        //    {
        //        await Application.Current.MainPage.DisplayAlert(
        //            Languages.ErrorDialogTitle,
        //            response.Message,
        //            Languages.AcceptDialogButton);
        //        return;
        //    }

        //    var documentTypes = (List<DocumentType>)response.Result;
        //    DocumentTypesCollection = new ObservableCollection<DocumentType>(documentTypes);
        //}
    }
}
