using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Services;
using GoodWorkersMVP.ViewModels;

namespace GoodWorkersMVP.Models
{
    public class Ocupation
    {
        NavigationService navigationService;

        public int OcupationId { get; set; }

        public string OcupationName { get; set; }

        public List<User> Users { get; set; }

        public override int GetHashCode()
        {
            return OcupationId;
        }

        public ICommand SelectOcupationCommand
        {
            get
            {
                return new RelayCommand(SelectOcupation);
            }
        }

        public Ocupation()
        {
            navigationService = new NavigationService();
        }

        async void SelectOcupation()
        {
            MainViewModel.GetInstance().Users = new UsersViewModel(Users);
            await navigationService.NavigateOnMaster("UsersPage");
        }
    }
}
