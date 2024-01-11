using GoodWorkersMVP.Models;
using GoodWorkersMVP.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodWorkersMVP.Helpers.Mocks
{
    public class UsersMock
    {
        public static List<UserModelMock> GetAllUsers()
        {
            return new List<UserModelMock>
            {
                new UserModelMock { 
                    Id = 1,
                    FirstName = "Felipe",
                    MiddleName = "Ernesto",
                    LastName = "Segoviano",
                    AboutMe = "What about me, and what about you... Bueno esto es solo un texto de prueba",
                    Address = "Calle Luna, Calle Sol #345, Centro Ciudad",
                    Birthday = DateTime.Now.AddYears(-28),
                    Age = 37,
                    Cellphone = "849-849-8449",
                    DocumentTypeId = 1,
                    DocumentNumber = 00220601708,
                    Phone = "809-809-8009"
                },
                new UserModelMock {
                    Id = 2,
                    FirstName = "Maria",
                    MiddleName = "Cristina",
                    LastName = "Marichal",
                    AboutMe = "What about me, and what about you... Bueno esto es solo un texto de prueba",
                    Address = "Calle Luna, Calle Sol #345, Centro Ciudad",
                    Birthday = DateTime.Now.AddYears(-32),
                    Age = 41,
                    Cellphone = "849-849-8449",
                    DocumentTypeId = 1,
                    DocumentNumber = 00220601963,
                    Phone = "809-809-8009"
                },
            };
        }

        public static UserModelMock GetUserId(int userId)
        {
            if (userId == 1)
            {
                return new UserModelMock
                {
                    Id = 1,
                    FirstName = "Felipe",
                    MiddleName = "Ernesto",
                    LastName = "Segoviano",
                    AboutMe = "What about me, and what about you... Bueno esto es solo un texto de prueba",
                    Address = "Calle Luna, Calle Sol #345, Centro Ciudad",
                    Birthday = DateTime.Now.AddYears(-28),
                    Age = 37,
                    Cellphone = "849-849-8449",
                    DocumentTypeId = 1,
                    DocumentNumber = 00220601708,
                    Phone = "809-809-8009",
                    ProfileImage = "https://t3.ftcdn.net/jpg/03/07/57/54/360_F_307575473_NaZ8XNxe1BBt5Z0fKgMZWJgb1JIzDuYR.jpg"
                };
            }
            else if (userId == 2)
            {
                return new UserModelMock
                {
                    Id = 2,
                    FirstName = "Maria",
                    MiddleName = "Cristina",
                    LastName = "Marichal",
                    AboutMe = "What about me, and what about you... Bueno esto es solo un texto de prueba",
                    Address = "Calle Luna, Calle Sol #345, Centro Ciudad",
                    Birthday = DateTime.Now.AddYears(-32),
                    Age = 41,
                    Cellphone = "849-849-8449",
                    DocumentTypeId = 1,
                    DocumentNumber = 00220601963,
                    Phone = "809-809-8009",
                    ProfileImage = "https://media.gettyimages.com/id/1460896518/photo/beautiful-middle-aged-woman.jpg?s=612x612&w=0&k=20&c=ZMf5qdN536EUx9QlqMDARwHCckYcX9UHGJedgREpOAE="
                };
            }
            else
            {
                return null;
            }
        }    
    }
}
