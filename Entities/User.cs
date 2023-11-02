using System.Collections.Generic;
using Newtonsoft.Json;
namespace HPW.Entities
{
    public class User
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        public int Streak { get; set; }
        public int Level { get; set; }

        public int Coins { get; set; }

        public List<User> Friends { get; set; }
        public List<Reward> Rewards { get; set; }
        public List<Invite> Invites { get; set; }
    }
}
