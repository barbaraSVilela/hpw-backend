using System;
using System.Collections;
using System.Collections.Generic;
class User
{
    public int id {get; set;}
    public string email{get;set;}
    public string name{get;set;}
    public int streak{get;set;}
    public int level{get;set;}

    public int coins{get;set}

    public List<User> friends;
    public List<Reward> rewards;
    public List<Invite> invites;
}