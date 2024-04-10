using System;
using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QueueFunctionApp.Models;
using QueueToTableFunctionApp.Models;

namespace QueueFunctionApp
{
    public class QueueFunction
    {
        private readonly ILogger _logger;

        public QueueFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<QueueFunction>();
        }

        [Function(nameof(QueueFunction))]
        [TableOutput("Produtos", Connection = "queueConnection")]
        public TableProduto Run([QueueTrigger("queuetrigger", Connection = "queueConnection")] Produto product)
        {
            TableProduto tableProduto = new TableProduto()
            {
                PartitionKey = product.Categoria,
                RowKey = product.Id.ToString(),
                Descricao = product.Descricao
            };

            //Produto? product = JsonConvert.DeserializeObject<Produto>(message);
            _logger.LogInformation(product!.ToString());
            return tableProduto;

        }
    }
}
