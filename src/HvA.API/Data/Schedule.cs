using Newtonsoft.Json;

namespace HvA.API.Data
{
    public class Schedule
    {

        [JsonProperty("Description")]
        public string Description { get; internal set; }

        [JsonProperty("HostKey")]
        public string HostKey { get; internal set; }

        [JsonProperty("Value")]
        public string Value { get; internal set; }

        [JsonProperty("TableType")]
        public string TableType { get; internal set; }

        [JsonProperty("Key")]
        public string Key { get; internal set; }

    }
}
