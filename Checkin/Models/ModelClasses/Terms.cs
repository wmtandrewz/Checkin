using System;
using Newtonsoft.Json;

namespace Checkin.Models.ModelClasses
{
    public class Terms
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("Hotel")]
        public int Hotel { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Order")]
        public int Order { get; set; }
    }
}
