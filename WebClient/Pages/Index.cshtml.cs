using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using WebClient.Models;

namespace WebClient.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public string UserName = string.Empty;
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet()
    {
        try
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetStringAsync("https://localhost:7011/person");

            if(!string.IsNullOrWhiteSpace(response))
            {
                var person = JsonConvert.DeserializeObject<PersonModel>(response);
                UserName = $"{person.FirstName} {person.LastName}";
            }
        }
        catch (Exception ex){
            _logger.LogError(ex.Message);
        }
    }
}
