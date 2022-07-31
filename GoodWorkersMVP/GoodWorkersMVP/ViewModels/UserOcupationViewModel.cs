using GoodWorkersMVP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GoodWorkersMVP.ViewModels
{
    public class UserOcupationViewModel : BaseViewModel
    {
        private ObservableCollection<User> users;

        public Ocupation Ocupation { get; set; }

        public ObservableCollection<User> Users
        {
            get => this.users;
            set => this.SetValue(ref this.users, value);
        }

        public UserOcupationViewModel(Ocupation ocupation)
        {
            this.Ocupation = ocupation;

            //this.LoadUserOcupation();
        }

        private void LoadUserOcupation()
        {
            this.Users = new ObservableCollection<User>();

            IEnumerable<User> query = from user in this.Ocupation.Users
                                      where user.Id == this.Ocupation.OcupationId
                                      select user;

            foreach (var user in this.Ocupation.Users)
            {
                //var ocupation = MainViewModel.GetInstance()
                //                             .OcupationList
                //                             .Where(u => u.OcupationId == user.OcupationId).FirstOrDefault();

                //IEnumerable<User> query = from 

                //if (ocupation != null)
                //{
                //    this.Users.Add(new UsersOcupation
                //    {
                //        FirstName = ocupation.FirstName
                //    });
                //}
            }
        }
    }
}
