using System;

namespace HPW.Entities
{
    public class DailyChallenge
    {
        [JsonProperty(PropertyName = "ChallengeId")]
        public int challengeId { get; set; }
        public DateTime Date { get; set; }

    }
}

