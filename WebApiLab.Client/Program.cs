using System.Text.Json;
using WebApiLab.Client.Api;

Console.Write("ProductId: ");
var id = Console.ReadLine();

if (id != null)
{
    //await GetProductAsync(int.Parse(id));
    var p = await GetProduct2Async(int.Parse(id));
    Console.WriteLine($"{p.Name}: {p.UnitPrice}");
}

Console.ReadKey();

static async Task<Product> GetProduct2Async(int id)
{
    using var httpClient = new HttpClient()
    { BaseAddress = new Uri("http://localhost:5184/") };
    var client = new ProductClient(httpClient);
    return await client.GetAsync(id);
}

static async Task GetProductAsync(int id)
{
    using var client = new HttpClient();
    var response = await client.GetAsync(new Uri($"http://localhost:5184/api/Product/{id}"));
    response.EnsureSuccessStatusCode();
    var jsonStream = await response.Content.ReadAsStreamAsync();
    var json = await JsonDocument.ParseAsync(jsonStream);
    Console.WriteLine($"{json.RootElement.GetProperty("name")}:" + $"{json.RootElement.GetProperty("unitPrice")}.-");
}