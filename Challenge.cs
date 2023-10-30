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

namespace HPW
{
    public static class Challenge
    {
        [FunctionName("challenge")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            DailyChallenge response = new DailyChallenge();

            response.id = 1;
            response.prompt = "Faça imprimir Hello World";
            response.level = 1;
            response.coins = 2;
            response.solution = new List<string>();
            response.solution.Add("System");
            response.solution.Add(".");
            response.solution.Add("out");
            response.solution.Add("println");
            response.solution.Add("(");
            response.solution.Add("Hello World ");
            response.solution.Add(")");
            response.solution.Add(";");
            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(response);
        }
    }
}
