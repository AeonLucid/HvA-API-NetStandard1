using Newtonsoft.Json;

namespace HvA.API.NetStandard1.Data
{
    public class AuthenticationUser
    {
        
        [JsonProperty("IsAuthenticated")]
        public bool IsAuthenticated { get; internal set; }

        [JsonProperty("User")]
        public User User { get; internal set; }

        [JsonProperty("Profile")]
        public Profile Profile { get; internal set; }

    }
}
