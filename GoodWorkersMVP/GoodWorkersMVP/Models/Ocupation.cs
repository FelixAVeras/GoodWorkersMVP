using GoodWorkersMVP.Pages;
using GoodWorkersMVP.ViewModels;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace GoodWorkersMVP.Models
{
    public class Ocupation
    {
        public int Id { get; set; }

        public string OcupationName { get; set; }

        public List<User> Users { get; set; }

        public int SelectedUserId { get; set; }

        public ICommand SelectOcupationCommand => new RelayCommand(SelectOcupation);

        async void SelectOcupation()
        {
            MainViewModel.GetInstance().Ocupation = this;
            MainViewModel.GetInstance().SelectedUserId = SelectedUserId;
            MainViewModel.GetInstance().Users = new UsersViewModel(Users);

            await App.Navigator.PushAsync(new UsersPage());
        }
    }
}
