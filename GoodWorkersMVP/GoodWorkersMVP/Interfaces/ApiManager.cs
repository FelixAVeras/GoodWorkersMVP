using GoodWorkersMVP.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodWorkersMVP.Interfaces
{
    public class ApiManager : IApiManager
    {
        private List<User> _mockUsers;

        public ApiManager()
        {
            _mockUsers = new List<User>
            {
                new User() 
                { 
                    Id = 1, 
                    FirstName = "Juan", 
                    MiddleName = "", 
                    LastName = "Perez Ceballos",
                    Birthday = DateTime.Now.AddYears(-28),
                    Address = "Calle Luna Calle sol #33",
                    AboutMe = "Esto es solo un texto de prueba",
                    Cellphone = "849-849-8449",
                    Phone = "849-849-8449",
                    Email = "jperez28@yopmail.com",
                    DocumentTypeId = 1,
                    DocumentNumber = "001-0689748-8",
                    OcupationID = 2,
                },
                new User() 
                { 
                    Id = 2, 
                    FirstName = "Pedro", 
                    MiddleName = "", 
                    LastName = "Perez Ceballos",
                    Birthday = DateTime.Now.AddYears(-28),
                    Address = "Calle Luna Calle sol #33",
                    AboutMe = "Esto es solo un texto de prueba",
                    Cellphone = "849-849-8449",
                    Phone = "849-849-8449",
                    Email = "jperez28@yopmail.com",
                    DocumentTypeId = 1,
                    DocumentNumber = "001-0689748-8",
                    OcupationID = 1,
                },
                new User() 
                { 
                    Id = 3, 
                    FirstName = "Maria", 
                    MiddleName = "", 
                    LastName = "Perez Ceballos",
                    Birthday = DateTime.Now.AddYears(-28),
                    Address = "Calle Luna Calle sol #33",
                    AboutMe = "Esto es solo un texto de prueba",
                    Cellphone = "849-849-8449",
                    Phone = "849-849-8449",
                    Email = "jperez28@yopmail.com",
                    DocumentTypeId = 1,
                    DocumentNumber = "001-0689748-8",
                    OcupationID = 3,
                },
                new User()
                {
                    Id = 4,
                    FirstName = "Amelia",
                    MiddleName = "",
                    LastName = "Perez Ceballos",
                    Birthday = DateTime.Now.AddYears(-28),
                    Address = "Calle Luna Calle sol #33",
                    AboutMe = "Esto es solo un texto de prueba",
                    Cellphone = "849-849-8449",
                    Phone = "849-849-8449",
                    Email = "jperez28@yopmail.com",
                    DocumentTypeId = 1,
                    DocumentNumber = "001-0689748-8",
                    OcupationID = 4,
                },
            };
        }

        public async Task<List<Ocupation>> GetOcupations()
        {
            await Task.Delay(2000);

            var ocupations = new List<Ocupation>
            {
                new Ocupation() { Id = 1, OcupationName = "Albañil" },
                new Ocupation() { Id = 2, OcupationName = "Plomero" },
                new Ocupation() { Id = 3, OcupationName = "Electricista" },
                new Ocupation() { Id = 4, OcupationName = "Servicio Domestico" }
            };

            return ocupations;
        }
        public Task<List<User>> GetOcupationById(int id)
        {
            return Task.FromResult(_mockUsers.FindAll(user => user.OcupationID == id));
        }

        public Task<User> GetUser(int id)
        {
            return Task.FromResult(_mockUsers.Find(u => u.OcupationID == id));
        }
    }
}
