using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class Program
    {
        static int Menu()
        {
            int Option;
            Console.WriteLine("*********************************** Menu ***********************************");
            Console.WriteLine("Op(1): Navigate");
            Console.WriteLine("Op(2): Add a beer");
            Console.WriteLine("Op(3): Exit");
            Console.WriteLine("****************************************************************************");
            Console.Write("Your option: ");
            Option = Int32.Parse(Console.ReadLine());
            return Option;
        }

        static void beersWithTheSameStyleAsThis(int beerId, string uri, HttpClient httpClient, RootObjectBeers beerInfo) 
        {
            string entryPoint = uri + beerInfo._embedded.beer[beerId - 1]._links.style.href + "/beers";
            var response = httpClient.GetAsync(entryPoint).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var results = (JObject)JsonConvert.DeserializeObject(data);
            int totalOfBeers = (int)results["TotalResults"];

            if(totalOfBeers == 0)
            {
                Console.WriteLine("There are no other beers with this style.");
            }
            else if (totalOfBeers == 1)
            {
                Console.WriteLine("This is the only beer having this style.");
            }
            else
            {
                Console.WriteLine("There are " + totalOfBeers + " beers with this style.");
            }
        }

        static void Main(string[] args)
        {
            int option;
            do
            {
                Console.Clear();
                option = Menu();
                switch (option)
                {
                    case 1:

                        string uri = "http://datc-rest.azurewebsites.net";
                        var client = new HttpClient();
                        client.DefaultRequestHeaders.Add("accept", "application/hal+json");
                        string entryPoint = uri + "/breweries/";
                        var response = client.GetAsync(entryPoint).Result;


                        var data = response.Content.ReadAsStringAsync().Result;
                        var result = (JObject)JsonConvert.DeserializeObject(data);
                        var BreweriesInfo = (RootObject)result; 

                        int BreweryNumber = BreweriesInfo._embedded.brewery.Count();
                        int BreweryId = 0;
                        var beerInfo = new RootObjectBeers(0, 0, 0, new BeerEmbedded(null));
                        int BeerId = 0;

                        do {
                            Console.Clear();
                            Console.WriteLine("There are informations for a total amount of " + BreweryNumber + " breweries.");
                            Console.WriteLine("Which one of the breweries would you like to explore? (1, 2, 3... or " + BreweryNumber + ")");
                            Console.Write("The wanted id: ");
                            BreweryId = Int32.Parse(Console.ReadLine());
                        } while (BreweryId <= 0 || BreweryId > BreweryNumber);

                        Console.WriteLine();
                        Console.WriteLine("Brewery name: " + BreweriesInfo._embedded.brewery[BreweryId - 1].Name);
                        Console.Write("Press any key..");
                        Console.ReadLine();

                        do {
                            Console.Clear();
                            Console.WriteLine("*************** List of beers from the brewery " + BreweryId + " ***************");
                            string apiBeers = uri + BreweriesInfo._embedded.brewery[BreweryId - 1]._links.beers.href;
                            var responseApi = client.GetAsync(apiBeers).Result;
                            var dataApi = responseApi.Content.ReadAsStringAsync().Result;
                            var resultApi = (JObject)JsonConvert.DeserializeObject(dataApi);
                            beerInfo = (RootObjectBeers)resultApi;
                            Console.WriteLine("There exists " + beerInfo._embedded.beer.Count() + " beers available at the brewery " + BreweriesInfo._embedded.brewery[BreweryId - 1].Name + ".");
                            Console.WriteLine("Which one of them do you want to explore? ");
                            Console.Write("The wanted id: ");
                            BeerId = Int32.Parse(Console.ReadLine());
                        } while (BeerId <= 0 || BeerId > beerInfo._embedded.beer.Count());

                        Console.WriteLine();
                        Console.WriteLine("Beer name: " + beerInfo._embedded.beer[BeerId - 1].Name);
                        Console.WriteLine("Beer style: " + beerInfo._embedded.beer[BeerId - 1].StyleName);
                        beersWithTheSameStyleAsThis(BeerId, uri, client, beerInfo);
                        Console.Write("Press any key..");
                        Console.ReadLine();

                        break;
                    case 2:
                        client = new HttpClient();
                        Console.Clear();
                        Console.WriteLine("What beer do you want to add?");
                        Console.WriteLine("The name of the beer: ");
                        string AddedBeerName = Console.ReadLine();

                        string beer = "{\"Name\":\"" + AddedBeerName + "\"}";
                        var postResponse = client.PostAsJsonAsync("http://datc-rest.azurewebsites.net/beers", beer);
                        
                        break;
                    case 3:
                        return;
                }
            } while (option != 0);
        }
    }
}
