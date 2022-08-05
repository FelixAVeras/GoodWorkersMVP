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

        /**** Literals ****/

        //Dialogs
        public static string ErrorTitleDialog => Resource.ErrorTitleDialog;
        public static string ErrorEmailEmptyLabel => Resource.ErrorEmailEmptyLabel;
        public static string ErrorEmailInvalidLabel => Resource.ErrorEmailInvalidLabel;
        public static string ErrorPasswordEmptyLabel => Resource.ErrorPasswordEmptyLabel;
        public static string NoInternetDialog => Resource.NoInternetDialog;
        public static string NoIntertnetWorkingDialog => Resource.NoIntertnetWorkingDialog;
        public static string UnableDetecLocation => Resource.UnableLocation;
        public static string SomethingWentWrong => Resource.SomethingWentWrong;
        public static string BtnAcceptDialog => Resource.BtnAcceptDialog;
        public static string BtnCancelDialog => Resource.BtnCancelDialog;
        public static string FirstNameValidationEmpty => Resource.FirstNameValidationEmpty;
        public static string LastNameValidationEmpty => Resource.LastNameValidationEmpty;
        public static string AddressValidationEmpty => Resource.AddressValidationEmpty;
        public static string CellphoneValidationEmpty => Resource.CellphoneValidationEmpty;
        public static string DocumentNumberValidationEmpty => Resource.DocumentNumberValidationEmpty;
        public static string AboutMeValidationEmpty => Resource.AboutMeValidationEmpty;
        public static string PasswordValidationCorrect => Resource.PasswordValidationCorrect;
        public static string PasswordValidationConfirm => Resource.PasswordValidationConfirm;
        public static string RegisterConfirmationTitleDialog => Resource.RegisterConfirmationTitleDialog;
        public static string RegisterConfirmationMessage => Resource.RegisterConfirmationMessage;

        //Forms
        public static string EmailLabel => Resource.EmailLabel;
        public static string EmailPlaceholder => Resource.EmailPlaceholder;
        public static string PasswordLabel => Resource.PasswordLabel;
        public static string PasswordPlaceholder => Resource.PasswordPlaceholder;
        public static string SearchPLaceholderInput => Resource.SearchPLaceholderInput;

        //Login
        public static string LoginTitlePage => Resource.LoginTitlePage;
        public static string RememberMyDataLabel => Resource.RememberMyDataLabel;
        public static string ForgetPasswordLabel => Resource.ForgetPasswordLabel;
        public static string BtnEnterLabel => Resource.BtnEnterLabel;
        public static string BtnRegisterLabel => Resource.BtnRegisterLabel;

        //Register
        public static string RegisterTitlePage => Resource.RegisterTitlePage;

        //Miselaneous
        public static string UsersTitlePage => Resource.UsersTitlePage;
    }
}
