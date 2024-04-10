using AppWebMvc.Models;

namespace AppWebMvc.Services
{
    public interface IPessoasClientService
    {
        Task<IEnumerable<Pessoa>> ListarPessoas();
    }
}