using AppWebApi.Models;

namespace AppWebApi.Services
{
    public class PessoasService : IPessoasService
    {
        public IEnumerable<Pessoa> ListarPessoas()
        {
            return new List<Pessoa>()
            {
                new Pessoa() { Id = 10, Nome = "Tony Santos", Idade = 23 },
                new Pessoa() { Id = 20, Nome = "Susan Freitas", Idade = 35 },
                new Pessoa() { Id = 30, Nome = "Jeff Thomson", Idade = 28 }
            };
        }
    }
}