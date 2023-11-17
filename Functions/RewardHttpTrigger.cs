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
        private readonly IUserService _userService;
        public RewardHttpTrigger(
            IRewardService rewardService,
            IUserService userService
        )
        {
            _rewardService = rewardService;
            _userService = userService;
        }


        [FunctionName("GetRewards")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rewards")] HttpRequest req,
            ILogger log)
        {

            var rewards = await _rewardService.GetAllRewards();


            return new OkObjectResult(rewards);
        }

        [FunctionName("PurchaseReward")]
        public async Task<IActionResult> PurchaseReward(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "purchase")] HttpRequest req,
            ILogger log,
            [AuthToken] User user)
        {
            if (user == null)
            {
                return new UnauthorizedObjectResult("Token not provided");
            }
            var completeUser = await _userService.CompleteUserInformation(user);
            var rewardId = req.Query["rewardId"];

            try
            {
                var updatedUser = await _rewardService.PurchaseReward(rewardId, completeUser);
                return new OkObjectResult(updatedUser);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
