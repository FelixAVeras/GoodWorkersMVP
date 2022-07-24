using GoodWorkersMVP.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GoodWorkersMVP.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private ObservableCollection<User> users { get; set; }

        public Ocupation Ocupation { get; set; }

        public ObservableCollection<User> Users
        {
            get { return this.users; }
            set { this.SetValue(ref this.users, value); }
        }

        public UsersViewModel(Ocupation ocupation)
        {
            this.Ocupation = ocupation;
            LoadUsers();
        }

        private void LoadUsers()
        {
            this.Users = new ObservableCollection<User>();

            foreach(var user in this.Ocupation.Users)
            {
                var oc = MainViewModel.GetInstance().OcupationList
                                                    .Where(o => o.Users == )
            }
        }
    }
}
