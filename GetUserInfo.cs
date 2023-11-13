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
    public class GetUserInfo
    {
         private readonly IUserService _userService;

         public GetUserInfo(IUserService userService){
            _userService = userService;
         }


        [FunctionName("GetUserInfo")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user")] HttpRequest req,
            ILogger log,
            [AuthToken] User user)
        {
            if (user == null)
            {
                return new UnauthorizedObjectResult("Token not provided");
            }

            var completeUser = await _userService.CompleteUserInformation(user);

            return new OkObjectResult(completeUser);
        }
    }
}
