using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using HPW.Bindings.Attributes;
using HPW.Entities;
using HPW.Services;

namespace HPW.Functions
{
    public class RewardHttpTrigger
    {
        private readonly IRewardService _rewardService;
        public RewardHttpTrigger(
            IRewardService rewardService
        )
        {
            _rewardService = rewardService;
        }


        [FunctionName("GetRewards")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rewards")] HttpRequest req,
            ILogger log)
        {

            var rewards = await _rewardService.GetAllRewards();


            return new OkObjectResult(rewards);
        }
    }
}
