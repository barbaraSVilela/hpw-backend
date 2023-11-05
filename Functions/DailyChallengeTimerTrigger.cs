using HPW.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace HPW.Functions
{
    public class DailyChallengeTimerTrigger
    {
        private readonly IDailyChallengeService _dailyChallengeService;

        public DailyChallengeTimerTrigger(IDailyChallengeService dailyChallengeService)
        {
            _dailyChallengeService = dailyChallengeService;
        }


        [FunctionName("DailyChallengeTimerTrigger")]
        public void Run([TimerTrigger("0 0 0 * * ?")] TimerInfo myTimer, ILogger log)
        {
            _dailyChallengeService.SetTodaysChallenges();
        }
    }
}
