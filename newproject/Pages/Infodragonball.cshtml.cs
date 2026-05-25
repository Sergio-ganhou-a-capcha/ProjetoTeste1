using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using newproject.Models;
using System.Text.Json;

namespace newproject.Pages
{
    public class InfoDragonBallModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InfoDragonBallModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public DragonBall InfoDragonBall { get; set; }

        public string id { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            this.id = id;

            var client = _httpClientFactory.CreateClient("RestDragonBall");

            // pedir à API com a seguinte route, em que enviamos o 'cod' recebido
            var response = await client.GetAsync($"characters/{id}");

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
            var artigoResponse = JsonSerializer.Deserialize<DragonBallApiResponse>(json, options);

            // a maneira de como colocamos a InfoDragonBall com os dados recebidos também é diferente
            InfoDragonBall = new DragonBall
            {
                UniqueId = artigoResponse.id.ToString(),
                OfficialName = artigoResponse.name,
                imageUrl = artigoResponse.image,
                affiliation = artigoResponse.affiliation
            };

            return Page();
        }
    }
}