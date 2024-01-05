using System;
using System.Collections.Generic;
using System.Text;

namespace GoodWorkersMVP.Helpers.Mocks
{
    public class UserModelMock
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AboutMe { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public string Cellphone { get; set; }
        public int DocumentTypeId { get; set; }
        public long DocumentNumber { get; set; }
        public string Phone { get; set; }
        public string ProfileImage { get; set; }

        public string FullName { get; set; }
        public string Ocupation { get; set; }
    }
}
