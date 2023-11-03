using System;
using System.Collections;
using System.Collections.Generic;

namespace HPW.Entities
{
    public class Challenge
    {
        
        [JsonProperty(PropertyName = "ChallengeId")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "prompt")]
        public string prompt { get; set; }

        [JsonProperty(PropertyName = "level")]
        public int level { get; set; }

        [JsonProperty(PropertyName = "coins")] 
        public int coins { get; set; }

        [JsonProperty(PropertyName = "solution")]
        public List<string> solution { get; set; }

        [JsonProperty(PropertyName = "options")]
        public List<string> options { get; set; }
    }
}


