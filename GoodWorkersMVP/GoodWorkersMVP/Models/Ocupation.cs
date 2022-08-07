using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.ViewModels;
using System.Collections.Generic;
using System.Windows.Input;

namespace GoodWorkersMVP.Models
{
    public class Ocupation
    {
        public int Id { get; set; }

        public string OcupationName { get; set; }

        public List<User> Users { get; set; }

        public ICommand SelectOcupationCommand
        {
            get
            {
                return new RelayCommand(SelectOcupation);
            }
        }

        async void SelectOcupation()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Ocupation = this;
            mainViewModel.Users = new UsersViewModel(Users);
            await App.Navigator.PushAsync(new UsersPage());
        }
    }
}
