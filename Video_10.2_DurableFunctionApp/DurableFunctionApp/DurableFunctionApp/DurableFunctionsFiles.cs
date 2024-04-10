using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace DurableFunctionApp
{
    public static class DurableFunctionsFiles
    {
        private static void GerarFatura(string msg, double valor)
        {
            StreamWriter writer = new StreamWriter(@"D:\Azure\log.txt", true);
            saldo += valor;
            writer.WriteLine($"Mensagem: {msg} - Valor: {valor:c} - Saldo: {saldo:c}");
            writer.Close();
        }

        private static double saldo = 0;

        [Function(nameof(GetCash))]
        public static string GetCash([ActivityTrigger] string name, FunctionContext executionContext)
        {
            ILogger log = executionContext.GetLogger(nameof(GetCash));
            log.LogInformation($"Executando a função ATM {name}.");
            GerarFatura("Cash", 100);
            return $"Cash  {name} witdrawn!";
        }

        [Function(nameof(GetItems))]
        public static string GetItems([ActivityTrigger] string name, FunctionContext executionContext)
        {
            ILogger log = executionContext.GetLogger(nameof(GetItems));
            log.LogInformation($"Executando a função GetItems {name}.");
            GerarFatura("Compra", 200);
            return $"Bought {name}!";
        }

        [Function(nameof(SaveCash))]
        public static string SaveCash([ActivityTrigger] string name, FunctionContext executionContext)
        {
            ILogger log = executionContext.GetLogger(nameof(SaveCash));
            log.LogInformation($"Executando a função SaveCash {name}.");
            GerarFatura("Deposito", 300);
            return $"Deposited money in {name}!";
        }

        [Function(nameof(RunOrchestrator))]
        public static async Task<List<string>> RunOrchestrator([OrchestrationTrigger] TaskOrchestrationContext context)
        {
            var outputs = new List<string>();
            outputs.Add(await context.CallActivityAsync<string>(nameof(GetCash), "200"));
            outputs.Add(await context.CallActivityAsync<string>(nameof(GetItems), "Potato"));
            outputs.Add(await context.CallActivityAsync<string>(nameof(SaveCash), "Citibank"));

            return outputs;
        }

        [Function(nameof(HttpStart))]
        public static async Task<HttpResponseData> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("HttpStart");

            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(RunOrchestrator));
            logger.LogInformation("Created new orchestration with instance ID = {instanceId}", instanceId);

            return client.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
