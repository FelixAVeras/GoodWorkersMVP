using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodWorkersMVP.Models
{
    public class DocumentType
    {
        [JsonProperty("document_type_id")]
        public int DocumentTypeID { get; set; }

        [JsonProperty("document_type_name")]
        public string DocumentTypeName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
