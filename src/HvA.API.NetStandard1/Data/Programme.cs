using Newtonsoft.Json;

namespace HvA.API.NetStandard1.Data
{
    public class Programme
    {

        [JsonProperty("Naam")]
        public string Name { get; internal set; }

        [JsonProperty("AZUrl")]
        public string AzUrl { get; internal set; }

    }
}
