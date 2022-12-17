// See https://aka.ms/new-console-template for more information
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebClient.Models;

Console.WriteLine("Started....");

var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

if (disco.IsError)
{
    Console.WriteLine("DiscoveryDoc error : " + disco.Error);    
}

var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,
    ClientId = "WebClient",
    ClientSecret = "secret-value",
    Scope = "PersonAPI"
});

if (tokenResponse.IsError)
{
    Console.WriteLine("Token error : " + tokenResponse.Error);
}

// Console.WriteLine(tokenResponse.Json);

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:7011/person");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine("Api call error : " + response.StatusCode);
}
else
{
    var content = await response.Content.ReadAsStringAsync();
    var objPerson = JsonConvert.DeserializeObject<Person>(content);


    Console.WriteLine($"Hello {objPerson.FirstName} {objPerson.LastName}.");
}


Console.Write($"{Environment.NewLine}Press any key to exit...");
Console.ReadKey(true);