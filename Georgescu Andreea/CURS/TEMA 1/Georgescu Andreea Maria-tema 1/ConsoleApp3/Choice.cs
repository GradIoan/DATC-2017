using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApp3
{
    class Choice
    {
        public static void getBreweries(RootObj rootObj)
        {
            foreach (var brewerieElement in rootObj.Embedded.BreweriesList)
                Console.WriteLine(brewerieElement.Name);
        }
        public static void getBeers(RootObj rootObj)
        {
            Console.WriteLine(" Give the Brewery name to see the beers ");
            string BreweryName = Console.ReadLine();
            foreach (var a in rootObj.Embedded.BreweriesList)
            {
                if (a.Name == BreweryName)
                {
                    var data=Connect.Connect1(a.LinksClass.Hrefobj3.href);
                    var items = JsonConvert.DeserializeObject<Beerclass>(data);
                    if (items.TotalResults != 0)
                    {
                        Console.WriteLine("Beers: ");
                        foreach (var beerElement in items.ResourceList)
                            Console.WriteLine(beerElement.Name);
                    }
                    else
                    {
                        Console.WriteLine("The breweries has no beers");
                    }
                }
            }
        }
        public static void postBreweries(int id, string name,string url)
        { 
            BeerList r = new BeerList(name, id);
            StringContent c = new StringContent(JsonConvert.SerializeObject(r));
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            c.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var clientResponse = client.PostAsync(url, c).Result;
            Console.WriteLine(clientResponse);
        }
    }
}
