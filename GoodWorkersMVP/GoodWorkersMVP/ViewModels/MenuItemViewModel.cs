using GalaSoft.MvvmLight.Command;
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

        public ICommand NavigateCommand
        {
            get { return new RelayCommand(Navigate); }
        }

        private void Navigate()
        {
            App.Master.IsPresented = false;

            //if (this.PageName == "UserProfilePage")
            //{
            //    MainViewModel.GetInstance().UserProfileVireModel = new UserProfileViewModel();
            //    App.Navigator.PushAsync(new UserProfilePage());
            //}

            //if (this.PageName == "MapsPage")
            //{
            //    //MainViewModel.GetInstance().MapsViewModel = new MapsViewModel();
            //    App.Navigator.PushAsync(new MapsPage());
            //}

            if (this.PageName == "LoginPage")
            {
                var mainViewModel = MainViewModel.GetInstance();

                //mainViewModel.Token = string.Empty;
                //mainViewModel.TokenType = string.Empty;

                //Settings.Token = string.Empty;
                //Settings.TokenType = string.Empty;

                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
