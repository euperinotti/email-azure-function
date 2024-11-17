using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace euperinotti.azure
{
    public class HttpTrigger
    {
        private readonly ILogger<HttpTrigger> _logger;

        public HttpTrigger(ILogger<HttpTrigger> logger)
        {
            _logger = logger;
        }

        [Function("HttpTrigger")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonSerializer.Deserialize<RequestData>(requestBody);

                if (data == null || string.IsNullOrEmpty(data.Email))
                {
                    return new BadRequestObjectResult("Email is required.");
                }

                _logger.LogInformation($"Email received: {data.Email}");

                SendEmail.Execute(data.Email).Wait();
                _logger.LogInformation("Email sent successfully");

                return new OkObjectResult($"Email sent to {data.Email}!");
            }
            catch (System.Exception)
            {
                _logger.LogInformation("Something went wrong!");
            }

            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
