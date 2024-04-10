using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueFunctionApp.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string? Categoria { get; set; }
        public string? Descricao { get; set; }
        public double Preco { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\nCategoria: {Categoria}\nDescrição: {Descricao}\nPreço: {Preco:C}";
        }
    }
}
