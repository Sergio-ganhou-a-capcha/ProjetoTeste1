using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using newproject.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace newproject.Pages;

public class ListdragonballModel : PageModel{
    private readonly IHttpClientFactory _httpClientFactory;


    public ListdragonballModel(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
    }


    public List<DragonBall> DragonBalles { get; set; } = new();

    public async Task OnGetAsync(){

        //var client = _httpClientFactory.CreateClient();
        //var response = await client.GetAsync("https://restcountries.com/v3.1/all");
        var client = _httpClientFactory.CreateClient("RestDragonBall");
        var response = await client.GetAsync("characters/?race=Saiyan&affiliation=Z fighter&fields=id,name,image");

        if (response.IsSuccessStatusCode){
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dados = JsonSerializer.Deserialize<List<DragonBallApiResponse>>(json, options);

            DragonBalles = dados.Select(d => new DragonBall{
                UniqueId = d.id.ToString(),
                OfficialName = d.name,
                imageUrl = d.image
            }).ToList();

        }

    }
  /*

private readonly ILogger<IndexModel> _logger;
    public IndexModel(ILogger<IndexModel> logger){
        _logger = logger;
    }
    public void OnGet(){
    }
*/

}
