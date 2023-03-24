using Newtonsoft.Json;

namespace RestSharp_Project.DataModel
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