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
        public ChallengeHttpTrigger(
            IUserService userService
        )
        {
            _userService = userService;
        }


        [FunctionName("GetChallenge")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "challenge")] HttpRequest req,
            ILogger log,
            [AuthToken] User user)
        {
            var completeUser = await _userService.CompleteUserInformation(user);
            // log.LogInformation("C# HTTP trigger function processed a request.");

            // string name = req.Query["name"];

            // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            // dynamic data = JsonConvert.DeserializeObject(requestBody);
            // name = name ?? data?.name;



            // string responseMessage = string.IsNullOrEmpty(name)
            //     ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //     : $"Hello, {name}. This HTTP triggered function executed successfully.";

            // var responseMessage = $"The logged user is: {user.Email}";

            return new OkObjectResult(completeUser);
        }
    }
}
