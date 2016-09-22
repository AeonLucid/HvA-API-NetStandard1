using Newtonsoft.Json;

namespace HvA.API.NetStandard1.Data
{
    public class StudyLocationPage
    {

        [JsonProperty("HtmlContent")]
        public string ContentHtml { get; internal set; }

        [JsonProperty("PreviousPage")]
        public string PreviousPage { get; internal set; }

    }
}
