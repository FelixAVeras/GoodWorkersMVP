using GoodWorkersMVP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GoodWorkersMVP.ViewModels
{
    public class MainViewModel
    {
        //Properties
        public LoginViewModel Login { get; set; }
        public RegisterViewModel Register { get; set; }

        public OcupationViewModel Ocupations { get; set; }
        public Ocupation Ocupation { get; set; }
        public UsersViewModel Users { get; set; }

        public List<Ocupation> OcupationList { get; set; }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();

            LoadMenu();
        }

        static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;

        }

        private void LoadMenu()
        {
            Menus = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel()
                {
                    Icon = "internetexplorer",
                    PageName = "MapsPage",
                    TitleIcon = "Explorar"
                },

                new MenuItemViewModel()
                {
                    Icon = "usericon",
                    PageName = "UserProfilePage",
                    TitleIcon = "Mi Pefíl"
                },

                new MenuItemViewModel()
                {
                    Icon = "chaticon",
                    PageName = "ChatPage",
                    TitleIcon = "Mensajes"
                },

                new MenuItemViewModel()
                {
                    Icon = "gearoption",
                    PageName = "SettingsPage",
                    TitleIcon = "Configuración"
                },

                new MenuItemViewModel()
                {
                    Icon = "logout",
                    PageName = "LoginPage",
                    TitleIcon = "Cerrar Sesión"
                }
            };
        }

        
    }
}
