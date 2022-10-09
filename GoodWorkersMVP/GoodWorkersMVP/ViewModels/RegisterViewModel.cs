using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
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

        private ApiService apiService;
        private ImageSource imageSource;
        private MediaFile file;
        private bool isRunning;
        private bool isEnable;

        public ImageSource ImageSource
        {
            get => this.imageSource;
            set => SetValue(ref this.imageSource, value);
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

        public ICommand ChangeUserPhotoCommand => new RelayCommand(ChangeUserPhoto);
        public ICommand RegisterCommand => new RelayCommand(Register);

        public RegisterViewModel()
        {
            apiService = new ApiService();
            imageSource = "camera";

            this.IsEnable = true;
        }

        private async void ChangeUserPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable &&
                CrossMedia.Current.IsTakePhotoSupported)
            {
                var source = await Application.Current.MainPage.DisplayActionSheet(
                    Languages.TakePhotoSource,
                    Languages.BtnCancelDialog,
                    null,
                    Languages.FromGallery,
                    Languages.FromCamera);

                if (source == Languages.BtnCancelDialog)
                {
                    this.file = null;
                    return;
                }

                if (source == Languages.FromCamera)
                {
                    this.file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Sample",
                            Name = "test.jpg",
                            PhotoSize = PhotoSize.Small,
                        }
                    );
                }
                else
                {
                    this.file = await CrossMedia.Current.PickPhotoAsync();
                }
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }

        private async void Register()
        {
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

            if (string.IsNullOrEmpty(Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.AddressValidationEmpty,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(Cellphone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.CellphoneValidationEmpty,
                    Languages.BtnAcceptDialog);

                return;
            }

            //TODO: Validacion de la fecha vacia y para mayoria de edad

            if (string.IsNullOrEmpty(DocumentNumber))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.DocumentNumberValidationEmpty,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(AboutMe))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.AboutMeValidationEmpty,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.ErrorEmailEmptyLabel,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (!RegexUtilities.isValidEmail(this.Email))
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

            if (this.Password.Length < 6 && this.Password.Length > 20)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.PasswordValidationCorrect,
                    Languages.BtnAcceptDialog);

                return;
            }

            if (this.Password != this.PasswordConfirm)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ErrorTitleDialog,
                    Languages.PasswordValidationConfirm,
                    Languages.BtnAcceptDialog);
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
                    Languages.ErrorTitleDialog,
                    checkConnetion.Message,
                    Languages.BtnAcceptDialog);
                return;
            }

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FileHelper.ReadFull(this.file.GetStream());
            }

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

            var apiurl = Application.Current.Resources["urlApi"].ToString();


            this.IsRunning = false;
            this.IsEnable = true;

            await Application.Current.MainPage.DisplayAlert(
                Languages.RegisterConfirmationTitleDialog,
                Languages.RegisterConfirmationMessage,
                Languages.BtnAcceptDialog);

            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
