using Newtonsoft.Json;

namespace HvA.API.NetStandard1.Data
{
    public class TimetableItem
    {

        [JsonProperty("StartDate")]
        public long StartDate { get; internal set; }

        [JsonProperty("EndDate")]
        public long EndDate { get; internal set; }

        [JsonProperty("StartTime")]
        public string StartTime { get; internal set; }

        [JsonProperty("EndTime")]
        public string EndTime { get; internal set; }

        [JsonProperty("ModuleCode")]
        public string ModuleCode { get; internal set; }

        [JsonProperty("ActivityDescription")]
        public string ActivityDescription { get; internal set; }

        [JsonProperty("Locations")]
        public TimetableLocation[] Locations { get; internal set; }

        [JsonProperty("StaffMembers")]
        public string[] StaffMembers { get; internal set; }

        [JsonProperty("StudentSets")]
        public string[] StudentSets { get; internal set; }

        [JsonProperty("Location")]
        public string Location { get; internal set; }

        [JsonProperty("Teachers")]
        public string Teachers { get; internal set; }

        [JsonProperty("Groups")]
        public string Groups { get; internal set; }

    }
}
