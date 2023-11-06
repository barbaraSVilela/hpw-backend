using HPW.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPW.Services
{
    public interface IChallengeService
    {
        Task<List<Challenge>> GetAllChallenges();
        Task<Challenge> GetChallenge(string challengeId);

        Task<Dictionary<int, List<Challenge>>> GetAllChallengesSplitByLevel();

    }
}
