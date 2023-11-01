using System;
using System.Collections;
using System.Collections.Generic;

namespace HPW.Entities
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Prompt { get; set; }
        public int Level { get; set; }
        public int Coins { get; set; }
        public List<string> Solution { get; set; }
        public List<string> Options { get; set; }
    }
}


