using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HPW.Entities
{
    public class Challenge
    {
        
        [JsonProperty(PropertyName = "ChallengeId")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "prompt")]
        public string Prompt { get; set; }

        [JsonProperty(PropertyName = "level")]
        public int Level { get; set; }

        [JsonProperty(PropertyName = "coins")] 
        public int Coins { get; set; }

        [JsonProperty(PropertyName = "solution")]
        public List<string> Solution { get; set; }

        [JsonProperty(PropertyName = "options")]
        public List<string> Options { get; set; }
    }
}


