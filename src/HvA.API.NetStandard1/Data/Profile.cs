using Newtonsoft.Json;

namespace HvA.API.NetStandard1.Data
{
    public class Profile
    {

        [JsonProperty("Domain")]
        public string Domain { get; set; }

        [JsonProperty("DomainAZUrl")]
        public string DomainAzUrl { get; set; }

        [JsonProperty("Language")]
        public string Language { get; set; }

        //  TODO: Add unknown properties.
        //  [JsonProperty("Courses")]
        //
        //  [JsonProperty("Teachers")]

        [JsonProperty("AZProgrammeTitle")]
        public string AzProgrammeTitle { get; set; }

        [JsonProperty("AZType")]
        public string AzType { get; set; }

        [JsonProperty("AZPrefix")]
        public string AzPrefix { get; set; }

        [JsonProperty("IsComplete")]
        public string IsComplete { get; set; }

    }
}
