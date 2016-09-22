using Newtonsoft.Json;

namespace HvA.API.Data
{
    public class Domain
    {

        [JsonProperty("Code")]
        public string Code { get; internal set; }

        [JsonProperty("Naam")]
        public string Name { get; internal set; }

        [JsonProperty("Active")]
        public string Active { get; internal set; }

    }
}
