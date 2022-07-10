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
        public UsersViewModel Users { get; set; }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();

            this.LoadMenu();
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
            this.Menus = new ObservableCollection<MenuItemViewModel>();

            this.Menus.Add(new MenuItemViewModel()
            {
                Icon = "internetexplorer",
                PageName = "MapsPage",
                TitleIcon = "Explorar"
            });

            this.Menus.Add(new MenuItemViewModel()
            {
                Icon = "usericon",
                PageName = "UserProfilePage",
                TitleIcon = "Mi Pefíl"
            });

            this.Menus.Add(new MenuItemViewModel()
            {
                Icon = "chaticon",
                PageName = "ChatPage",
                TitleIcon = "Mensajes"
            });

            this.Menus.Add(new MenuItemViewModel()
            {
                Icon = "gearoption",
                PageName = "SettingsPage",
                TitleIcon = "Configuración"
            });

            this.Menus.Add(new MenuItemViewModel()
            {
                Icon = "logout",
                PageName = "LoginPage",
                TitleIcon = "Cerrar Sesión"
            });
        }

        
    }
}
