using AppWebApi.Models;
using AppWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoasService _pessoasService;

        public PessoasController(IPessoasService pessoasService)
        {
            this._pessoasService = pessoasService;
        }

        [HttpGet]
        public IEnumerable<Pessoa> GetPessoas()
        {
            return _pessoasService.ListarPessoas();
        }
    }
}