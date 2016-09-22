using Newtonsoft.Json;

namespace HvA.API.Data
{
    public class StudyLocation
    {

        [JsonProperty("Name")]
        public string Name { get; internal set; }

        [JsonProperty("Url")]
        public string Url { get; internal set; }

    }
}
