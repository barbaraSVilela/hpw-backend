using System;
using Newtonsoft.Json;

namespace HPW.Entities
{
    public class DailyChallenge
    {
        [JsonProperty(PropertyName = "ChallengeId")]
        public int ChallengeId { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "level")]
        public int Level { get; set; }

    }
}

