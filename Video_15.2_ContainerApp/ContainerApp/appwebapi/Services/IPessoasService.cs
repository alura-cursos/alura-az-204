using AppWebApi.Models;

namespace AppWebApi.Services
{
    public interface IPessoasService
    {
        IEnumerable<Pessoa> ListarPessoas();
    }
}