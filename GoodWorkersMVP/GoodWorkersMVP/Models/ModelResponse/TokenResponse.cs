using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodWorkersMVP.Models.ModelResponse
{
    public class TokenResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        public DateTime ExpiresIn { get; set; }

        public string ErrorDescription { get; set; }
    }
}
