using Newtonsoft.Json;

namespace HvA.API.NetStandard1.Data
{
    public class News
    {

        [JsonProperty("Id")]
        public int id { get; internal set; }

        [JsonProperty("Title")]
        public string Title { get; internal set; }

        [JsonProperty("CreatedOn")]
        public string CreatedOn { get; internal set; } // Date

        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; internal set; } // Date

        [JsonProperty("DlwoUrl")]
        public string DlwoUrl { get; internal set; }

    }
}
