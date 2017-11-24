using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Tema1
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowBeer(Beer beer)
        {
            Console.WriteLine($"Name: {beer.Name}");
        }

        static async Task<Uri> CreateBeerAsync(Beer beer)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("http://datc-rest.azurewebsites.net/beers/", beer);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Beer> GetBeerAsync(string path)
        {
            Beer beer = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                beer = await response.Content.ReadAsAsync<Beer>();
            }
            return beer;
        }

        static async Task<Beer> UpdateBeerAsync(Beer beer)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/beers/{beer.Id}", beer);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated beer from the response body.
            beer = await response.Content.ReadAsAsync<Beer>();
            return beer;
        }

        static async Task<HttpStatusCode> DeleteBeerAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/beers/{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            var client = new HttpClient();
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/beers/").Result;

            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject(data);

            Console.WriteLine(data);

           // Console.ReadLine();
             RunAsync().Wait();
       
        }

        static async Task RunAsync()
        {
            Beer beer;
            client.BaseAddress = new Uri("http://datc-rest.azurewebsites.net/beers/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string opt;
            try
            {
                Console.WriteLine($"---Meniu---");
                Console.WriteLine($"1. Create a new beer");
                Console.WriteLine($"2. Get the beer");
                Console.WriteLine($"3. Update the beer");
                Console.WriteLine($"4. Get the updated beer");
                Console.WriteLine($"5. Delete the beer");
                Console.WriteLine($"Introduceti optiunea voastra:   ");
               // opt = Console.ReadLine();
               // switch(opt)
               // {
               //     case "1":
                        beer = new Beer { Id="1", Name = "Timisoareana", Price=1, Category="Bere romaneasca" };
                        var url = await CreateBeerAsync(beer);
                        Console.WriteLine($"Created at {url}");
                   // break;
                    //case "2":
                        beer = await GetBeerAsync(url.PathAndQuery);
                        ShowBeer(beer);
                    //    break;
                    //case "3":
                        Console.WriteLine("Updating price...");
                        beer.Price = 80;
                        await UpdateBeerAsync(beer);
                    //break;
                    //case "4":
                        beer = await GetBeerAsync(url.PathAndQuery);
                        ShowBeer(beer);
                    //break;
                    //case "5":
                        var statusCode = await DeleteBeerAsync(beer.Id);
                        Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
                    //break;*/
                    //default:
                    //    Console.WriteLine($"Optiunea voastra este gresita");
                    //break;
                    //
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static Task<Beer> GetBeerAsync(Uri url)
        {
            throw new NotImplementedException();
        }
    }
}
