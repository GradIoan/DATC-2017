// http://datc-rest.azurewebsites.net/breweries
//http://datc-rest.azurewebsites.net/beers
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
        static int UserMenu()
        {
            int userOption;
            Console.WriteLine("========UserMenu========");
            Console.WriteLine("1) Explore API");
            Console.WriteLine("2) Add a new beer.");
            userOption = Int32.Parse(Console.ReadLine());
            return userOption;
        }

        static void Main(string[] args)
        {
            int option;
            do
            {
                option = UserMenu();
                switch (option)
                {
                    case 1:
                        string uri = "http://datc-rest.azurewebsites.net";
                        var client = new HttpClient();
                        client.DefaultRequestHeaders.Add("accept", "application/hal+json");
                        string entryPoint = uri + "/breweries/";
                        var response = client.GetAsync(entryPoint).Result; //get
                        var data = response.Content.ReadAsStringAsync().Result;
                        var result = (JObject)JsonConvert.DeserializeObject(data);
                        var berariiInfo = (RootObject)result; // mapare pe clasa RootObject

                        int NumarBerarii = berariiInfo._embedded.brewery.Count();
                        Console.WriteLine("Exista in total informatii pentru " + NumarBerarii + " berarii.");

                        Console.WriteLine("Care dintre ele doriti sa o explorati?(1, 2... sau " + NumarBerarii + ")");
                        Console.WriteLine("Id-ul dorit >");
                        int IdBerarie = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(berariiInfo._embedded.brewery[IdBerarie - 1].Name);

                        Console.WriteLine();
                        Console.WriteLine("Lista de beri de la beraria " + IdBerarie + ":");
                        string api2 = uri + berariiInfo._embedded.brewery[IdBerarie - 1]._links.beers.href;
                        var responseApi = client.GetAsync(api2).Result;
                        var dataApi = responseApi.Content.ReadAsStringAsync().Result;
                        var resultApi = (JObject)JsonConvert.DeserializeObject(dataApi);
                        var beerInfo = (RootObjectBeers)resultApi; // mapare pe clasa RootObjectBeers
                        Console.WriteLine("Exista " + beerInfo._embedded.beer.Count()+ " beri disponibile la beraria "+ berariiInfo._embedded.brewery[IdBerarie - 1].Name);
                        Console.WriteLine("Care dintre ele doriti sa o explorati?(1, 2... sau " + beerInfo._embedded.beer.Count() + ")");
                        Console.WriteLine("Id-ul dorit >");
                        int IdBere = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(beerInfo._embedded.beer[IdBere].BreweryName);

                        ////////Console.WriteLine();
                        ////////string api3 = uri + beerInfo._embedded.beer[IdBere]._links.self.href;
                        ////////var responseApi3 = client.GetAsync(api3).Result;
                        ////////var dataApi3 = responseApi3.Content.ReadAsStringAsync().Result;
                        ////////var resultApi3 = (JObject)JsonConvert.DeserializeObject(dataApi3);
                        ////////var beerInfo2 = (Beer)resultApi3; // mapare pe clasa RootObjectBeers

                        break;
                    case 2:
                        Console.WriteLine("Ce bere doriti sa adaugati?\n");
                        Console.WriteLine("Denumire bere >");
                        string denumireBereAdaugata = Console.ReadLine();

                        string bere = "{\"Name\":\"" + denumireBereAdaugata + "\"}";

                        string url = "http://datc-rest.azurewebsites.net/beers";
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";

                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(bere);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }                        
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();            
                        break;
                }
            } while (option != 0);
        }
    }
}
