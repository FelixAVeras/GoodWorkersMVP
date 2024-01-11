using GoodWorkersMVP.Helpers.Mocks;
using GoodWorkersMVP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GoodWorkersMVP.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private int _userId;

        public UserModelMock User { get; set; }

        public ProfileViewModel()
        {
            User = new UserModelMock
            {
                FullName = "Miguel Alcantara",
                AboutMe = "What about me, and what about you... Bueno esto es solo un texto de prueba",
                Address = "Calle Luna, Calle Sol #345, Centro Ciudad",
                Birthday = DateTime.Now.AddYears(-28),
                Age = 37,
                Cellphone = "849-849-8449",
                DocumentTypeId = 1,
                DocumentNumber = 00220601708,
                Phone = "809-809-8009",
                Ocupation = "Diseño de Interiores",
                ProfileImage = "https://t3.ftcdn.net/jpg/03/07/57/54/360_F_307575473_NaZ8XNxe1BBt5Z0fKgMZWJgb1JIzDuYR.jpg"
            };
        }
    }
}
