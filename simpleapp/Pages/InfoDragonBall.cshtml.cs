using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using simpleapp.Models;
using System.Text.Json;

namespace simpleapp.Pages
{
    public class InfoDragonBallModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InfoDragonBallModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public DragonBalls InfoDragonBall { get; set; }

        public string Id { get; set; }

        public async Task<IActionResult> OnGetAsync(string cod)
        {
            Id = cod;

            var client = _httpClientFactory.CreateClient("RestDragonBall");

            // pedir à API com a seguinte route, em que enviamos o 'cod' recebido
            var response = await client.GetAsync("/" + cod);

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

            // reparar que aqui não temos uma lista !!
            //var artigoResponse = JsonSerializer.Deserialize<CountryApiResponse>(json, options);
            var artigoResponse = JsonSerializer.Deserialize<List<DragonBallApiResponse>>(json, options)?.FirstOrDefault();

            // a maneira de como colocamos a InfoPais com os dados recebidos também é diferente
            InfoDragonBall = new DragonBalls
            {
                Id = artigoResponse.id,
                Fighter = artigoResponse.name,
                Description = artigoResponse.description,
                ImageURL = artigoResponse.image,
                Affiliation = artigoResponse.affiliation
            };

            return Page();
        }
    }
}
