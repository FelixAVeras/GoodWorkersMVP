using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodWorkersMVP.Models
{
    public class UserType
    {
        [JsonProperty("user_type_id")]
        public int UserTypeId { get; set; }

        [JsonProperty("user_type_name")]
        public string UserTypeName { get; set; }
    }
}
