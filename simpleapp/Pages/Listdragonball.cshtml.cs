using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using simpleapp.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace simpleapp.Pages;

public class Listdragonball : PageModel{
    private readonly IHttpClientFactory _httpClientFactory;

    public Listdragonball(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
    }

    public List<DragonBalls> Characters { get; set; } = new();

    public async Task OnGetAsync(){
        //var client = _httpClientFactory.CreateClient();
        //var response = await client.GetAsync("https://restcountries.com/v3.1/all");

        var client = _httpClientFactory.CreateClient("RestDragonBall");
        //var response = await client.GetAsync("/v3.1/all?fields=name,capital,currencies,cca2,flags ");

        var response = await client.GetAsync("?race=Saiyan&affiliation=Z fighter");

        if (response.IsSuccessStatusCode){
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dados = JsonSerializer.Deserialize<List<DragonBallApiResponse>>(json, options);
            //var dados = JsonSerializer.Deserialize<List<CountryApiResponse>>(json, options);

            Characters = dados.Select(d => new DragonBalls{
                Id = d.id,
                Fighter = d.fighter.name,
                ImageURL = d.image.png

            }).ToList();
        }
    }

    /*
private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
*/
}