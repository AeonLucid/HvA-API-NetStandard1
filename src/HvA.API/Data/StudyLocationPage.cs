using Newtonsoft.Json;

namespace HvA.API.Data
{
    public class StudyLocationPage
    {

        [JsonProperty("HtmlContent")]
        public string ContentHtml { get; internal set; }

        [JsonProperty("PreviousPage")]
        public string PreviousPage { get; internal set; }

    }
}
