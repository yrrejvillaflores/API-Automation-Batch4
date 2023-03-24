using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient_Project.DataModel
{
    public partial class TokenModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }

    public partial class LoginModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }


    }
}
