using AppWebMvc.Models;
using System.Net.Http.Headers;

namespace AppWebMvc.Services
{
    public class PessoasClientService : IPessoasClientService
    {
        private readonly HttpClient _httpClient;

        public PessoasClientService(IHttpClientFactory client)
        {
            _httpClient = client.CreateClient();

            //_httpClient.BaseAddress = new Uri("https://localhost:7225/");
            _httpClient.BaseAddress = new Uri("https://emiliocelso-back.internal.ambitiousground-01e6a3f8.brazilsouth.azurecontainerapps.io/");

            _httpClient.DefaultRequestHeaders.Accept.Clear();

            _httpClient.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<IEnumerable<Pessoa>> ListarPessoas()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/pessoas");
                if (response.IsSuccessStatusCode)
                {
                    var list = await response.Content.ReadFromJsonAsync<Pessoa[]>();
                    return list!.ToList();
                }
                else
                {
                    throw new Exception("Ocorreu um erro ao processar a lista.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
