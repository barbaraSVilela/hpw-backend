using HPW.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPW.Services
{
    public interface IDailyChallengeService
    {
        Task<DailyChallenge> GetTodaysChallenge(int level);
        Task SetTodaysChallenges();
    }
}
