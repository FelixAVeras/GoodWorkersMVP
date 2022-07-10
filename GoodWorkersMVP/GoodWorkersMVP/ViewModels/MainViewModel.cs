using System;
using System.Collections.Generic;
using System.Text;

namespace GoodWorkersMVP.ViewModels
{
    public class MainViewModel
    {
        //Properties
        public LoginViewModel Login { get; set; }
        public OcupationViewModel Ocupations { get; set; }
        public UsersViewModel Users { get; set; }

        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
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
    }
}
