using System;
using System.Collections;
using System.Collections.Generic;

namespace HPW.Entities
{
    public class ChallengeAttempt
    {
        public Challenge Challenge { get; set; }
        public User User { get; set; }
        public int Id { get; set; }
        public List<string> Attempt { get; set; }
    }
}

