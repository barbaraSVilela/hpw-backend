using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace HPW.Entities
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "streak")]
        public int Streak { get; set; }

        [JsonProperty(PropertyName = "level")]
        public int Level { get; set; }

        [JsonProperty(PropertyName = "coins")]
        public int Coins { get; set; }

        [JsonProperty(PropertyName = "friends")]
        public List<User> Friends { get; set; }

        [JsonProperty(PropertyName = "rewards")]
        public List<Reward> Rewards { get; set; }

        [JsonProperty(PropertyName = "invites")]
        public List<Invite> Invites { get; set; }

        [JsonProperty(PropertyName = "solvedChallenges")]
        public Dictionary<DateTime, string> SolvedChallenges { get; set; }

        [JsonProperty(PropertyName = "failedChallenges")]
        public Dictionary<DateTime, string> FailedChallenges { get; set; }
    }
}
