using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using newproject.Models;
using System.Text.Json;

namespace newproject.Pages
{
    public class InfopaisModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InfopaisModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Pais InfoPais { get; set; }

        public string CodigoPais { get; set; }

        public async Task<IActionResult> OnGetAsync(string cod)
        {
            CodigoPais = cod;

            var client = _httpClientFactory.CreateClient("RestCountries");

            // pedir à API com a seguinte route, em que enviamos o 'cod' recebido
            var response = await client.GetAsync($"v3.1/alpha/{cod}");

            if (!response.IsSuccessStatusCode)
            {
                // Artigo não encontrado ou erro na API
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // o endpoint /alpha retorna uma lista mesmo para um único país
            var artigoResponses = JsonSerializer.Deserialize<List<CountryApiResponse>>(json, options);
            if (artigoResponses == null || artigoResponses.Count == 0)
            {
                return NotFound();
            }

            var artigoResponse = artigoResponses[0];

            // a maneira de como colocamos a InfoPais com os dados recebidos também é diferente
            InfoPais = new Pais
            {
                OfficialName = artigoResponse.name?.official,
                Cca2 = artigoResponse.cca2,
                FlagUrl = artigoResponse.flags?.png
            };

            return Page();
        }
    }
}