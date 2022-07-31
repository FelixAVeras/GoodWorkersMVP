using System;
using System.Collections.Generic;
using System.Text;

namespace GoodWorkersMVP.Models
{
    public class UsersOcupation
    {
        public int OcupationID { get; set; }
        public string Ocupation { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}";
    }
}
