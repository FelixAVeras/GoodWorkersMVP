using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodWorkersMVP.Models
{
    internal class UserRequest
    {
        /*
         
           "firstName": "Felix",
  "lastName": "Carvajal Veras",
  "address": "Calle Principal 123",
  "cellphone": "555-123-4567",
  "birthday": "1990-05-25",
  "document_type_id": 1,
  "document_number": "123456789",
  "about_me": "Soy un apasionado por la tecnología y los viajes.",
  "ocupation_id": 2,
  "userType_id": 1,
  "device_name": "iPhone X",
  "username": "felixaveras",
  "email": "example@example.com",
  "password": "MiPasswordSeguro123",
  "password_confirmation": "MiPasswordSeguro123"
         
         */

        public int Id { get; set; }
        
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("middleName")]
        public string MiddleName { get; set; }
        
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("phone")]
        public string Phone { get; set; }
        
        [JsonProperty("cellphone")]
        public string Cellphone { get; set; }
        
        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }
        
        [JsonProperty("document_number")]
        public string DocumentNumber { get; set; }
        
        [JsonProperty("about_me")]
        public string AboutMe { get; set; }
        
        [JsonProperty("username")]
        public string UserName { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
        
        [JsonProperty("password_confirmation")]
        public string PasswordConfirmation { get; set; }
        
        [JsonProperty("document_type_id")]
        public long DocumentTypeId { get; set; }
        
        [JsonProperty("userType_id")]
        public long UserTypeId { get; set; }
        public UserType UserType { get; set; }
        
        [JsonProperty("ocupation_id")]
        public int OcupationID { get; set; }
        public Ocupation Ocupation { get; set; }
        
        [JsonProperty("")]
        public byte[] ImageArray { get; set; }
        
        [JsonProperty("device_name")]
        public string DeviceName { get; set; }
    }
}
