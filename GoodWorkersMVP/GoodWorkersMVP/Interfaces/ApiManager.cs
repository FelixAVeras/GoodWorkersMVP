using GoodWorkersMVP.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodWorkersMVP.Interfaces
{
    public class ApiManager : IApiManager
    {
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
    }
}
