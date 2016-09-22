using Newtonsoft.Json;

namespace HvA.API.Data
{
    public class Location
    {

        [JsonProperty("Code")]
        public int Code { get; internal set; }

        [JsonProperty("Naam")]
        public string Name { get; internal set; }

        [JsonProperty("Adres")]
        public string Address { get; internal set; }

        [JsonProperty("Postcode")]
        public string Postcode { get; internal set; }

        [JsonProperty("Plaats")]
        public string Plaats { get; internal set; }

        [JsonProperty("Telefoon")]
        public string Phone { get; internal set; }

        [JsonProperty("Openingstijden")]
        public string OpeningHoursHtml { get; internal set; }

        [JsonProperty("AanvullendeInfo")]
        public string ExtraInfoHtml { get; internal set; }

    }
}
