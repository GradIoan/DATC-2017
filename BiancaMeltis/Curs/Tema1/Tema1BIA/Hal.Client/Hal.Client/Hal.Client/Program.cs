// http://datc-rest.azurewebsites.net/breweries
//http://datc-rest.azurewebsites.net/beers
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Hal.Client
{
    class Program
    {

        static void Main(string[] args)
        {
            int option;
            do
            {
                Console.WriteLine("Meniu");
                Console.WriteLine("1) Parcurgere API");
                Console.WriteLine("2) Adauga o bere");
                Console.WriteLine("3) Exit");
                option = Int32.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                       // string uri = "http://datc-rest.azurewebsites.net";
                        var client = new HttpClient();
                        client.DefaultRequestHeaders.Add("accept", "application/hal+json");
                        string entryPoint = "http://datc-rest.azurewebsites.net" + "/breweries";
                        var response = client.GetAsync(entryPoint).Result; //get
                        var data = response.Content.ReadAsStringAsync().Result;
                        var result = (JObject)JsonConvert.DeserializeObject(data);
                        var berariiInfo = (RootObject)result; // mapare pe clasa RootObject

                        int NumarBerarii = berariiInfo._embedded.brewery.Count();                       
                        Console.WriteLine("Ce berarie vreti sa vizualizati?(1, 2... sau " + NumarBerarii + ")");                       
                        int IdBerarie = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Beraria aleasa:");
                        Console.WriteLine(berariiInfo._embedded.brewery[IdBerarie - 1].Name);
                        
                        Console.WriteLine();
                        Console.WriteLine("Lista de la beraria " + IdBerarie + ":");
                        string api2 = "http://datc-rest.azurewebsites.net" + berariiInfo._embedded.brewery[IdBerarie - 1]._links.beers.href;
                        var responseApi = client.GetAsync(api2).Result;
                        var dataApi = responseApi.Content.ReadAsStringAsync().Result;
                        var resultApi = (JObject)JsonConvert.DeserializeObject(dataApi);
                        var beerInfo = (RootObjectBeers)resultApi; // mapare pe clasa RootObjectBeers
                        Console.WriteLine("Au fost gasite " + beerInfo._embedded.beer.Count() +" beri:");
                        for (int i = 1; i <= beerInfo._embedded.beer.Count(); i++)
                        {
                            Console.WriteLine(beerInfo._embedded.beer[i-1].Name);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Ce bere doriti sa adaugati?");
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
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            } while (option != 0);
        }
    }
}
