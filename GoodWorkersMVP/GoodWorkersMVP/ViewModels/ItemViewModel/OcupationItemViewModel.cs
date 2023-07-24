using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels.ItemViewModel
{
    public class OcupationItemViewModel : Ocupation
    {
        public ICommand SelectOcupationCommand => new RelayCommand(SelectOcupation);

        private async void SelectOcupation()
        {
            //MainViewModel.GetInstance().OcupationUser = new OcupationUserViewModel(this);
            MainViewModel.GetInstance().Users = new UsersViewModel(this);
            await App.Navigator.PushAsync(new OcupationUserPage());
        }
    }
}
