using System.Threading.Tasks;
using HPW.Entities;

namespace HPW.Services
{
    public class DailyChallengeService : IDailyChallengeService
    {
        private readonly IChallengeService _challengeService;
        public DailyChallengeService(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        public Task<DailyChallenge> GetTodaysChallenge(int level)
        {
            throw new System.NotImplementedException();
        }

        public Task SetTodaysChallenges()
        {
            throw new System.NotImplementedException();
        }

    }
}