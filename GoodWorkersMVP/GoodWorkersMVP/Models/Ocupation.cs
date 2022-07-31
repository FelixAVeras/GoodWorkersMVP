using GalaSoft.MvvmLight.Command;
using GoodWorkersMVP.Pages;
using GoodWorkersMVP.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoodWorkersMVP.Models
{
    public class Ocupation
    {
        public int OcupationId { get; set; }

        public string OcupationName { get; set; }

        public List<User> Users { get; set; }

        public ICommand SelectOcupationCommand => new RelayCommand(SelectOcupation);

        async void SelectOcupation()
        {
            MainViewModel.GetInstance().Users = new UsersViewModel(Users);
            //await App.Navigator.PushAsync(new UsersPage());
            await Application.Current.MainPage.Navigation.PushAsync(new UsersPage());
        }
    }
}
