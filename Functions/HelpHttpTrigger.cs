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
    public class HelpHttpTrigger
    {
        private readonly IUserService _userService;
        private readonly IChallengeService _challengeService;
        public HelpHttpTrigger(
            IUserService userService,
            IChallengeService challengeService
        )
        {
            _userService = userService;
            _challengeService = challengeService;
        }


        [FunctionName("SendHelp")]
        public async Task<IActionResult> SendHelp(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "help")] HttpRequest req,
            ILogger log,
            [AuthToken] User user,
            [FromQuery] string data,
            [FromQuery] string challengeId)
        {

            if (user == null)
            {
                return new UnauthorizedObjectResult("Token not provided");
            }

            await _challengeService.AddHelpTip(challengeId, data);

            return new OkResult();
        }

    }
}
