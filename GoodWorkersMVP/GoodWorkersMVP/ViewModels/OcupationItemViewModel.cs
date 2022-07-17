using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
using System.Windows.Input;

namespace GoodWorkersMVP.ViewModels
{
    public class OcupationItemViewModel : Ocupation
    {
        public ICommand SelectOcupationCommand
        {
            get
            {
                return new RelayCommand(SelectOcupation);
            }
        }

        async void SelectOcupation()
        {
            MainViewModel.GetInstance().Users = new UsersViewModel();
            await App.Navigator.PushAsync(new UsersPage());
        }
    }
}
