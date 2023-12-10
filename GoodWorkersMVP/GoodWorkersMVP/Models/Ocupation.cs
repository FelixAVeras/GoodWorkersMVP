using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.Services;
using GoodWorkersMVP.ViewModels;
using System.Collections.Generic;
using System.Windows.Input;

namespace GoodWorkersMVP.Models
{
    public class Ocupation
    {
        private readonly OcupationService ocupationService;

        public int Id { get; set; }

        public string OcupationName { get; set; }

        public List<User> Users { get; set; }

        public ICommand SelectOcupationCommand => new RelayCommand(SelectOcupation);

        public Ocupation()
        {
            ocupationService = new OcupationService();
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
