using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodWorkersMVP.Models
{
    internal class UserRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
        public string ProfileImage { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string DocumentNumber { get; set; }
        public string AboutMe { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long DocumentTypeId { get; set; }
        public long OcupationId { get; set; }

        public UserType UserType { get; set; }
        public long UserTypeId { get; set; }

        public Ocupation Ocupation { get; set; }
        public int OcupationID { get; set; }

        public byte[] ImageArray { get; set; }

        public string DeviceName { get; set; }
    }
}
