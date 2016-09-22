using System;
using Newtonsoft.Json;

namespace HvA.API.Data
{
    public class Absentee
    {

        [JsonProperty("DisplayName")]
        public string DisplayName { get; internal set; }

        [JsonProperty("AbsentVanaf")]
        public DateTime From { get; internal set; }

        [JsonProperty("AbsentTot")]
        public DateTime To { get; internal set; }

        [JsonProperty("AbsentComment")]
        public string Comment { get; internal set; }

    }
}
