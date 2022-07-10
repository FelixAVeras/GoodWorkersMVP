using System;
using System.Collections.Generic;
using System.Text;

namespace GoodWorkersMVP.Models.ModelResponse
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }

        public string ErrorDescription { get; set; }
    }
}
