using HPW.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPW.Services
{
    public interface IDailyChallengeService
    {
        Task<Challenge> GetTodaysChallenge(User user);
        Task SetTodaysChallenges();
        Task<User> SolveTodaysChallenge(User user, bool wasSuccessful);
    }
}
