using Newtonsoft.Json;

namespace HvA.API.Data
{
    public class User
    {

        [JsonProperty("Username")]
        public string Username { get; internal set; }

        [JsonProperty("Email")]
        public string Email { get; internal set; }

        [JsonProperty("Token")]
        public string Token { get; internal set; }

        [JsonProperty("IsStudent")]
        public bool IsStudent { get; internal set; }

        [JsonProperty("IsEmployee")]
        public bool IsEmployee { get; internal set; }

    }
}
