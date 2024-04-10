using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QueueFunctionApp.Models;

namespace QueueFunctionApp
{
    public class FunctionQueue
    {
        private readonly ILogger<FunctionQueue> _logger;

        public FunctionQueue(ILogger<FunctionQueue> logger)
        {
            _logger = logger;
        }

        [Function(nameof(FunctionQueue))]
        public void Run([QueueTrigger("queuetrigger", Connection = "queueConnection")] string message)
        {
            Produto? product = JsonConvert.DeserializeObject<Produto>(message);
            _logger.LogInformation(product!.ToString());

            _logger.LogInformation($"\nC# Queue trigger function processed: {message}");
        }
    }
}
