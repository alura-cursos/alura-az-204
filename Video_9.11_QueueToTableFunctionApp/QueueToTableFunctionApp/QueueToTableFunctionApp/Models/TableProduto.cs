using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueToTableFunctionApp.Models
{
    public class TableProduto
    {
        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public string? Descricao { get; set; }

    }
}
