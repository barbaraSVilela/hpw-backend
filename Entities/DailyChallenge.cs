using System;
using Newtonsoft.Json;

namespace HPW.Entities
{
    public class DailyChallenge
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "challengeId")]
        public string ChallengeId { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "level")]
        public int Level { get; set; }

    }
}

