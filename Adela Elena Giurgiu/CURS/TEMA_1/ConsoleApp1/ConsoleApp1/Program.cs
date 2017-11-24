using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Breweries
{
    class Beer
    {
        public String Name { get; set; }
    }

    class Program
    {
        static void getBeers(int id, JObject obj)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            String uri = "http://datc-rest.azurewebsites.net";
            String req = uri + obj["_embedded"]["brewery"][id - 1]["_links"]["beers"]["href"];
            var resp = client.GetAsync(req).Result;
            var result = resp.Content.ReadAsStringAsync().Result;
            JObject beers = JObject.Parse(result);
            Console.WriteLine(beers);
        }
        static void getBrewery(int id, JObject obj)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            String uri = "http://datc-rest.azurewebsites.net";
            String req = uri + obj["_links"]["brewery"][id - 1]["href"];
            var resp = client.GetAsync(req).Result;
            var result = resp.Content.ReadAsStringAsync().Result;
            JObject brewery = JObject.Parse(result);
            Console.WriteLine(brewery);

            var option = -1;
            while (option != 0)
            {
                Console.WriteLine("0. Go back");
                Console.WriteLine("1. See beers");

                option = Convert.ToInt32(Console.ReadLine());
                if (option == 0)
                    return;
                else
                    getBeers(option, obj);
            }
        }
        static void Main(string[] args)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries").Result;

            var data = response.Content.ReadAsStringAsync().Result;
            // var obj = JsonConvert.DeserializeObject(data);
            JObject obj = JObject.Parse(data);

            int option = -2;
            while (option != -1)
            {
                int i = 0;
                Console.WriteLine("0. Post a beer");
                foreach (var brewery in obj["_links"]["brewery"])
                {
                    i++;
                    Console.WriteLine("{0}. {1} {2}", i, "Brewery", i);
                }

                option = Convert.ToInt32(Console.ReadLine());
                if (option == 0)
                {
                    Console.WriteLine("Please enter the name of the beer:");
                    var name = Console.ReadLine();
                    Beer beer = new Beer()
                    {
                        Name = name
                    };
                    var resp = client.PostAsJsonAsync("http://datc-rest.azurewebsites.net/beers", beer).Result;
                    Console.WriteLine(resp);
                }
                else
                    getBrewery(option, obj);
            }
        }
    }
}
