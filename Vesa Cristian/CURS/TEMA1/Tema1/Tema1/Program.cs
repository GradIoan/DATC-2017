using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tema1
{
    class Program
    {
        const string Url = "http://datc-rest.azurewebsites.net";
        static void Main(string[] args)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");

            var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries").Result;

            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<Endpoints>(data);

            var numberOfBreweries = obj.Links.Brewery.Count;
            var postBeer = string.Empty;
            if (obj.Embedded.Brewery.Count > 0 && obj.Embedded != null)
            {
                postBeer = "/" + obj.Embedded.Brewery[0].Links.Beers.Href.Split('/')[3];
            }


            int optiune = 0;
            do
            {
                Console.WriteLine("1.Afiseaza bere");
                Console.WriteLine("2.Adauga bere");
                Console.WriteLine("0.Iesire");
                Console.WriteLine("Alegeti optiunea:");
                optiune = Int32.Parse(Console.ReadLine());
                string url;

                switch (optiune)
                {
                    case 1:
                        Console.Write("Alegeti berarie[id]:");
                        int breweryId = 0;
                        int beerId = 0;
                        try
                        {
                            breweryId = int.Parse(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error at brewery id");
                            Console.ReadLine();
                            break;
                        }
                        
                        if (breweryId > numberOfBreweries)
                        {
                            Console.WriteLine("Nu exista id-ul berariei!");
                            Console.ReadLine();
                            break;
                        }

                        Console.Write("Alegeti berea[id]:");
                        try
                        {
                             beerId = int.Parse(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Nu exista id-ul berii!");
                            Console.ReadLine();
                            break;
                        }
                        

                        url = Program.Url + obj.Embedded.Brewery.First(x => x.Id == breweryId).Links.Beers.Href;

                        response = client.GetAsync(new Uri(url)).Result;
                        data = response.Content.ReadAsStringAsync().Result;
                        var beri = JsonConvert.DeserializeObject<BeersResponse>(data);

                        if (beerId > beri.Links.Beers.Count)
                        {
                            Console.WriteLine("Nu exista id-ul berii");
                            Console.ReadLine();
                            break;
                        }

                        url = Program.Url + beri.Links.Beers[beerId - 1].Href;
                        response = client.GetAsync(new Uri(url)).Result;
                        data = response.Content.ReadAsStringAsync().Result;
                        var beer = JsonConvert.DeserializeObject(data);
                        var jsonBeer = JsonConvert.SerializeObject(beer, Formatting.Indented);
                        Console.WriteLine(jsonBeer);
                        Console.ReadLine();

                        break;
                    case 2:
                        Beer bere = new Beer();
                        Console.Write("Id-ul berii:");
                        bere.Id = int.Parse(Console.ReadLine());
                        Console.Write("Id-ul berariei:");
                        bere.BreweryId = int.Parse(Console.ReadLine());
                        Console.Write("Numele berii:");
                        bere.Name = Console.ReadLine();
                        Console.Write("Numele berariei:");
                        bere.BreweryName = Console.ReadLine();
                        Console.Write("Stil id:");
                        bere.StyleId = int.Parse(Console.ReadLine());
                        Console.Write("Nume stil:");
                        bere.StyleName = Console.ReadLine();

                        var jsonBereFormat = JsonConvert.SerializeObject(bere, Formatting.Indented);
                        var httpContent = new StringContent(jsonBereFormat, Encoding.UTF8, "application/json");
                        url = Program.Url + postBeer;
                        var postResponse = client.PostAsync(url, httpContent).Result;
                        var responseString = postResponse.Content.ReadAsStringAsync();

                        if (postResponse.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            Console.WriteLine("Berea a fost adaugata");
                        }
                        Console.ReadLine();
                        break;
                    case 0:
                        Console.WriteLine("Iesire");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Optiunea aleasa nu exista");
                        Console.ReadLine();
                        break;
                }
                Console.Clear();
            } while (optiune != 0);
        }
    }
}
