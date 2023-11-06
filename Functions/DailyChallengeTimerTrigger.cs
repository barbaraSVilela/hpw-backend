using System.Threading.Tasks;
using HPW.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
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
        public async Task Run([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer, ILogger log)
        {
            await _dailyChallengeService.SetTodaysChallenges();
        }
    }
}
