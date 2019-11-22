using System;
using Newtonsoft.Json;

namespace Checkin.Models.ModelClasses
{
    public class DeviceModel
    {
        [JsonProperty("CurrentVersion")]
        public string CurrentVersion { get; set; }

        [JsonProperty("DeviceID")]
        public string DeviceID { get; set; }

        [JsonProperty("DeviceNo")]
        public int DeviceNo { get; set; }

        [JsonProperty("HotelCode")]
        public string HotelCode { get; set; }

        [JsonProperty("IsResgistered")]
        public bool IsResgistered { get; set; }

        [JsonProperty("LastRegisteredDate")]
        public DateTime LastRegisteredDate { get; set; }

        [JsonProperty("Notes")]
        public string Notes { get; set; }
    }
}
