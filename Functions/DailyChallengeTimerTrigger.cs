using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace HPW.Functions
{
    public class DailyChallengeTimerTrigger
    {
        [FunctionName("DailyChallengeTimerTrigger")]
        public void Run([TimerTrigger("0 0 0 * * ?")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
