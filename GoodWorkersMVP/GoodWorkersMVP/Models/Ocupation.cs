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
            MainViewModel.GetInstance().Users = new UsersViewModel(Users);
            //await Application.Current.MainPage.Navigation.PushAsync(new UsersPage());
            await App.Navigator.PushAsync(new UsersPage());
        }
    }
}
