using GoodWorkersMVP.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodWorkersMVP.Interfaces
{
    public interface IApiManager
    {
        Task<List<Ocupation>> GetOcupations();
        // Task<List<User>> GetUsers();
    }
}
