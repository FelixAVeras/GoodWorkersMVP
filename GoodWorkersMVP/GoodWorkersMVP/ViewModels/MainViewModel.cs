using GoodWorkersMVP.Helpers;
using GoodWorkersMVP.Helpers.Mocks;
using GoodWorkersMVP.Interfaces;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Models.ModelResponse;
using GoodWorkersMVP.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace GoodWorkersMVP.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        //Properties
        public TokenResponse Token { get; set; }

        public LoginViewModel Login { get; set; }
        public RegisterViewModel Register { get; set; }
        public OcupationViewModel Ocupations { get; set; }
        public Ocupation Ocupation { get; set; }
        public UsersViewModel Users { get; set; }
        public User User { get; set; }
        public ProfileViewModel Profile { get; set; }

        private int _selectedUserId;
        public int SelectedUserId
        {
            get => _selectedUserId;
            set => SetValue(ref _selectedUserId, value);
        }

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
                    Icon = "info",
                    PageName = "InfoPage",
                    TitleIcon = "Información"
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
