using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;


namespace Hal.Client
{
	class Program
	{
        static HttpClient client = new HttpClient();

        static void ShowBreweries(Breweries breweries)
        {
            Console.WriteLine($"Name: {breweries.Name}\t_Links: {breweries._links}");
        }

        static async Task<Uri> CreateBreweriesAsync(Breweries breweries)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("ResourceList", breweries);
            response.EnsureSuccessStatusCode();
            
            return response.Headers.Location;
        }

        static async Task<Breweries> GetBreweriesAsync(string path)
        {
            Breweries breweries = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                breweries = await response.Content.ReadAsAsync<Breweries>();
            }
            return breweries;
        }

        static async Task<Breweries> UpdateBreweriesAsync(Breweries breweries)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"ResourceList/{breweries.Id}", breweries);
            response.EnsureSuccessStatusCode();
            
            breweries = await response.Content.ReadAsAsync<Breweries>();
            return breweries;
        }

        static async Task<HttpStatusCode> DeleteBreweriesAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"ResourceList/{id}");
            return response.StatusCode;
        }

        static void Main(string[] args)
		{
            RunAsync().Wait();
		}

        static string url = "http://datc-rest.azurewebsites.net/breweries";
        static async Task RunAsync()
        {
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            while (true)
            {
                Console.WriteLine("1. Afiseaza berariile.");
                Console.WriteLine("2. Adauga o berarie.");
                Console.WriteLine("3. Exit.");
                Console.WriteLine("Introduceti optiunea: ");
                var opt = Convert.ToInt16(Console.ReadLine());
                switch (opt)
                {
                    case 1:
                        var response = client.GetAsync(url).Result;
                        var data = response.Content.ReadAsStringAsync().Result;
                        var obj = JsonConvert.DeserializeObject(data);
                        Console.WriteLine(obj);
                        break;

                    case 2:
                        Breweries brw = new Breweries();
                        Console.Write("Dati Id-ul berariei: ");
                        brw.Id = Console.ReadLine();
                        Console.Write("Dati numele berariei: ");
                        brw.Name = Console.ReadLine();
                        Console.Write("Dati link-ul berariei: ");
                        brw._links = Console.ReadLine();

                        var jsonBeerFormat = JsonConvert.SerializeObject(brw, Formatting.Indented);
                        var httpContent = new StringContent(jsonBeerFormat, Encoding.UTF8, "application/json");

                        var postResponse = await client.PostAsync(url, httpContent);
                        var responseString = await postResponse.Content.ReadAsStringAsync();

                        Console.WriteLine("Status code: " + postResponse.StatusCode);
                        if (postResponse.StatusCode == HttpStatusCode.Created)
                        {
                            Console.WriteLine("Berea a fost adaugata: \n" + responseString);
                        }
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}