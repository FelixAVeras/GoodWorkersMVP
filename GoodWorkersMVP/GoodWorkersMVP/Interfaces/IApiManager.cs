using GoodWorkersMVP.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodWorkersMVP.Interfaces
{
    //Mocking the app
    public interface IApiManager
    {
        Task<List<Ocupation>> GetOcupations();
        Task<List<User>> GetOcupationById(int id);
        Task<User> GetUser(int id);
    }
}
