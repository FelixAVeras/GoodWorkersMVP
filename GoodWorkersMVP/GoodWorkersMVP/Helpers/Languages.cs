using GoodWorkersMVP.Interfaces;
using System.Globalization;
using Xamarin.Forms;
using GoodWorkersMVP.Resources;

namespace GoodWorkersMVP.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        //Dialog validation
        public static string ErrorDialogTitle => Resource.LoginValidationErrorTitle;
        public static string AcceptDialogButton => Resource.DialogBtnAccept;
        public static string CancelDialogButton => Resource.DialogBtnCancel;
        public static string LoginValidationErrorMessage => Resource.LoginValidationErrorMessage;

        //Miscelaneous
        public static string CheckConnection => Resource.CheckConnection;
        public static string EnableConnection => Resource.EnableConnection;
        public static string RegisterConfirmationTitleDialog => Resource.RegisterConfirmationTitleDialog;
        public static string RegisterConfirmationMessage => Resource.RegisterConfirmationMessage;
        public static string CountryLabel => Resource.CountryLabel;
        public static string StateProvinceLabel => Resource.StateProvinceLabel;
        public static string SelectCountryPlaceHolder => Resource.SelectCountryPlaceHolder;
        public static string SelectStateProvincePlaceholder => Resource.SelectStateProvincePlaceholder;
        public static string SearchPlaceholder => Resource.SearchPlaceholder;

        // Login Page
        public static string LoginEmailPlaceholder => Resource.LoginEmailPlaceholder;
        public static string LoginPasswordPlaceholder => Resource.LoginPasswordPlaceholder;
        public static string LoginRememberData => Resource.LoginRememberData;
        public static string LoginForgetPassword => Resource.LoginForgetPassword;
        public static string LoginButtonEnter => Resource.LoginButtonEnter;
        public static string LoginButtonRegister => Resource.LoginButtonRegister;
        public static string LoginValidationEmailEmptyMessage => Resource.LoginValidationEmailEmptyMessage;
        public static string LoginValidationEmailInvalidMessage => Resource.LoginValidationEmailInvalidMessage;
        public static string LoginValidationPasswordEmpty => Resource.LoginValidationPasswordEmpty;
        public static string EmailPasswordInvalid => Resource.EmailPasswordInvalid;
        public static string LoginTitlePage => Resource.LoginTitlePage;

        // Register Page
        public static string RegisterPage => Resource.RegisterPage;
        public static string RegisterTitlePage => Resource.RegisterTitlePage;
        public static string ChangeImageProfileLabel => Resource.ChangeImageProfileLabel;
        public static string UserTypeTitleRegister => Resource.UserTypeTitleRegister;
        public static string UserTypeCustomer => Resource.UserTypeCustomer;
        public static string UserTypeProvider => Resource.UserTypeProvider;
        public static string RegisterFirstName => Resource.RegisterFirstName;
        public static string RegisterMiddleName => Resource.RegisterMiddleName;
        public static string RegisterLastName => Resource.RegisterLastName;
        public static string AddressLabel => Resource.AddressLabel;
        public static string CellPhoneLabel => Resource.CellPhoneLabel;
        public static string BirthdateLabel => Resource.BirthdateLabel;
        public static string DocumentTypeLabel => Resource.DocumentTypeLabel;
        public static string SelectDocumentTypeLabel => Resource.SelectDocumentTypeLabel;
        public static string DocumentNumberLabel => Resource.DocumentNumberLabel;
        public static string OcupationLabel => Resource.OcupationLabel;
        public static string SelectOcupationLabel => Resource.SelectOcupationLabel;
        public static string AboutMeLabel => Resource.AboutMeLabel;
        public static string AboutMePlaceholder => Resource.AboutMePlaceholder;
        public static string UsernameLabel => Resource.UsernameLabel;
        public static string RegisterEmailLabel => Resource.RegisterEmailLabel;
        public static string RegisterPasswordLabel => Resource.RegisterPasswordLabel;
        public static string ConfirmedPasswordLabel => Resource.ConfirmedPasswordLabel;
        public static string RegisterBtn => Resource.RegisterBtn;

        //New Validations for Register
        public static string AgeValidationMessage => Resource.AgeValidationMessage;
        public static string TermAndConditionValidationMessage => Resource.TermAndConditionValidationMessage;
        public static string AboutMeValidationLenght => Resource.AboutMeValidation;
        public static string AboutMeValidationEmpty => Resource.AboutMeValidationEmpty;
        public static string AddressValidationEmpty => Resource.AddressValidationEmpty;
        public static string AddressValidationLength => Resource.AddressValidationLength;
        public static string FirstNameValidationEmpty => Resource.FirstNameValidationEmpty;
        public static string FirstNameValidationLength => Resource.FirstnameValidationLength;
        public static string LastNameValidationEmpty => Resource.LastNameValidationEmpty;
        public static string LastNameValidationLength => Resource.LastNameValidationLength;
        public static string MiddleNameValidationLength => Resource.MiddleNameValidationLength;
        public static string DocumentNumberValidationEmpty => Resource.DocumentNumberEmpty;
        public static string CellphoneValidationEmpty => Resource.CellPhoneValidationEmpty;
        public static string UserNameValidationEmpty => Resource.UserNameValidationEmpty;
        public static string EmailValidationCorrect => Resource.EmailValidationCorrect;
        public static string PasswordValidationCorrect => Resource.PasswordValidationCorrect;
        public static string PasswordValidationConfirm => Resource.PasswordValidationConfirm;
        public static string TakePickFrom => Resource.TakePicTitleDialog;
        public static string TakePicDialogCamera => Resource.TakePicDialogCamera;
        public static string TakePicDialogGallery => Resource.TakePicDialogGallery;
        public static string TermAndConditionsLabel => Resource.TermAndConditionsLabel;
    }
}
