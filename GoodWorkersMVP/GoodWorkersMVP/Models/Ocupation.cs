using System.Collections.Generic;

namespace GoodWorkersMVP.Models
{
    public class Ocupation
    {
        public int OcupationId { get; set; }

        public string OcupationName { get; set; }

        public List<User> Users { get; set; }
    }
}
