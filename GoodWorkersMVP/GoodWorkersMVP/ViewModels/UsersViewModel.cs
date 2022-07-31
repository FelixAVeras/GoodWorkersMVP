using GoodWorkersMVP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace GoodWorkersMVP.ViewModels
{
    public class UsersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        List<User> users { get; set; }
        ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Users)));
                }
            }
        }

        public UsersViewModel(List<User> users)
        {
            this.users = users;

            Users = new ObservableCollection<User>(users.OrderBy(u => u.FullName));
        }
    }
}
