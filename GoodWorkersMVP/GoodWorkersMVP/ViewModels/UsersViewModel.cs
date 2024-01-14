using GoodWorkersMVP.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GoodWorkersMVP.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        List<User> users;

        ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get => _users;
            set => SetValue(ref _users, value);
        }

        public UsersViewModel(List<User> users)
        {
            this.users = users;
            Users = new ObservableCollection<User>(users.OrderBy(u => u.FirstName));
        }
    }
}

