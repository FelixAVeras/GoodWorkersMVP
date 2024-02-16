using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class MenuItemViewModel
    {
        public string Icon { get; set; }
        public string TitleIcon { get; set; }
        public string PageName { get; set; }

        public ICommand NavigateCommand => new RelayCommand(Navigate);

        private void Navigate()
        {
            App.Master.IsPresented = false;

            switch (PageName) 
            {
                //case "UserProfilePage":
                //    MainViewModel.GetInstance().Profile = new ProfileViewModel();
                //    App.Navigator.PushAsync(new ProfilePage());
                //    break;

                case "MapsPage":
                    App.Navigator.PushAsync(new MapsPage());
                    break;

                case "InfoPage":
                    App.Navigator.PushAsync(new InfoPage());
                    break;

                case "LoginPage":
                    Settings.AccessToken = string.Empty;
                    Settings.TokenType = string.Empty;
                    Settings.IsRemember = false;

                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }
    }
}
