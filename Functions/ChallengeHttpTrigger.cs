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
    public class ChallengeHttpTrigger
    {
        private readonly IUserService _userService;
        private readonly IDailyChallengeService _dailyChallengeService;
        public ChallengeHttpTrigger(
            IUserService userService,
            IDailyChallengeService dailyChallengeService
        )
        {
            _userService = userService;
            _dailyChallengeService = dailyChallengeService;
        }


        [FunctionName("GetChallenge")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "challenge")] HttpRequest req,
            ILogger log,
            [AuthToken] User user)
        {
            log.LogInformation("entered function");
            if (user == null)
            {
                log.LogInformation("user is null");
                return new UnauthorizedObjectResult("Token not provided");
            }

            var completeUser = await _userService.CompleteUserInformation(user);
            log.LogInformation("get user worked", completeUser);


            var todaysChallenge = await _dailyChallengeService.GetTodaysChallenge(completeUser.Level);

            log.LogInformation("today challenge", todaysChallenge);

            return new OkObjectResult(todaysChallenge);
        }
    }
}
