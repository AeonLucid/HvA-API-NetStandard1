using Newtonsoft.Json;

namespace HvA.API.Data
{
    public class TimetableLocation
    {

        [JsonProperty("id")]
        public string Id { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }

    }
}
