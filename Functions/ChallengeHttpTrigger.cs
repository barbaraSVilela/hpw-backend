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

            if (user == null)
            {
                return new UnauthorizedObjectResult("Token not provided");
            }

            var completeUser = await _userService.CompleteUserInformation(user);


            var todaysChallenge = await _dailyChallengeService.GetTodaysChallenge(completeUser);

            if (todaysChallenge == null)
            {
                return new BadRequestObjectResult("Sorry, no challenges were found for you today. Check again tomorrow!");
            }

            return new OkObjectResult(todaysChallenge);
        }

        [FunctionName("SolveChallenge")]
        public async Task<IActionResult> SolveChallenge(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "solve")] HttpRequest req,
            ILogger log,
            [AuthToken] User user)
        {

            if (user == null)
            {
                return new UnauthorizedObjectResult("Token not provided");
            }

            var wasSuccessful = bool.Parse(req.Query["wasSuccessful"]);

            var completeUser = await _userService.CompleteUserInformation(user);


            var updatedUser = await _dailyChallengeService.SolveTodaysChallenge(completeUser, wasSuccessful);

            return new OkObjectResult(updatedUser);
        }
    }
}
