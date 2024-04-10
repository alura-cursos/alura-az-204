using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json.Serialization;



namespace FunctionAppTrigger
{
    public class Order
    {
        public string? OrderId { get; set; }
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class FunctionHttp
    {
        private readonly ILogger<FunctionHttp> _logger;

        public FunctionHttp(ILogger<FunctionHttp> logger)
        {
            _logger = logger;
        }

        [Function("FunctionHttp")]
        public async Task<object> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            string? jsonContent = await req.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Order>(jsonContent);

            return new OkObjectResult($"Seu pedido foi processado sob o número {order.OrderId}");
        }
    }
}
