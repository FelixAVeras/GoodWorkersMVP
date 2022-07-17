using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using GoodWorkersMVP.Models;

namespace GoodWorkersMVP.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        List<User> users;

        ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetValue(ref _users, value); }
        }

        //public UsersViewModel(List<User> users)
        //{
        //    this.users = users;
        //    Users = new ObservableCollection<User>(users.OrderBy(u => u.FullName));
        //}

        public UsersViewModel()
        {
            //this.users = users;
            Users = new ObservableCollection<User>(users.OrderBy(u => u.FullName));
        }
    }
}
