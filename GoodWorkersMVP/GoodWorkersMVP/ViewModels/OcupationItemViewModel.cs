using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.ViewModels
{
    public class OcupationItemViewModel : Ocupation
    {
        public ICommand SelectOcupationCommand => new RelayCommand(SelectOcupation);

        async void SelectOcupation()
        {
            MainViewModel.GetInstance().Users = new UsersViewModel(this);
            await App.Navigator.PushAsync(new UsersPage());
        }
    }
}
