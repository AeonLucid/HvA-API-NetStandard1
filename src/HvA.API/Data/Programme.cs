using Newtonsoft.Json;

namespace HvA.API.Data
{
    public class Programme
    {

        [JsonProperty("Naam")]
        public string Name { get; internal set; }

        [JsonProperty("AZUrl")]
        public string AzUrl { get; internal set; }

    }
}
