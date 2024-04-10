using AppWebMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppWebMvc.Controllers
{
    public class PessoasController : Controller
    {
        private readonly IPessoasClientService _pessoasClientService;

        public PessoasController(IPessoasClientService pessoasClientService)
        {
            _pessoasClientService = pessoasClientService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var lista = await _pessoasClientService.ListarPessoas();
                return View(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
