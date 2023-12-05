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
    public class UserHttpTrigger
    {
        private readonly IUserService _userService;
        public UserHttpTrigger(
            IUserService userService
        )
        {
            _userService = userService;
        }


        [FunctionName("SendInvite")]
        public async Task<IActionResult> SendInvite(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "invite")] HttpRequest req,
            ILogger log,
            [AuthToken] User user)
        {

            if (user == null)
            {
                return new UnauthorizedObjectResult("Token not provided");
            }

            var completeUser = await _userService.CompleteUserInformation(user);

            var toUserId = req.Query["userId"];

            await _userService.SendInvite(toUserId, completeUser);


            return new OkResult();
        }

        [FunctionName("AcceptInvite")]
        public async Task<IActionResult> AcceptInvite(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "accept-invite")] HttpRequest req,
            ILogger log,
            [AuthToken] User user)
        {

            if (user == null)
            {
                return new UnauthorizedObjectResult("Token not provided");
            }

            var completeUser = await _userService.CompleteUserInformation(user);

            var inviteId = req.Query["inviteId"];

            await _userService.AcceptInvite(inviteId, completeUser);

            return new OkResult();
        }

        [FunctionName("GetFriends")]
        public async Task<IActionResult> GetFriends(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "friends")] HttpRequest req,
            ILogger log,
            [AuthToken] User user)
        {

            if (user == null)
            {
                return new UnauthorizedObjectResult("Token not provided");
            }

            var completeUser = await _userService.CompleteUserInformation(user);


            var result = await _userService.GetFriends(completeUser);

            return new OkObjectResult(result);
        }

    }
}
