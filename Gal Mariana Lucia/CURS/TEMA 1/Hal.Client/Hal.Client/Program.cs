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
        static void Main(string[] args)
        {
            ClasaMeniu meniu = new ClasaMeniu();
            int opt;
            do
            {
                opt = meniu.Meniu();
                switch (opt)
                {
                    case 1:
                        string uri = "http://datc-rest.azurewebsites.net";
                        var client = new HttpClient();
                        client.DefaultRequestHeaders.Add("accept", "application/hal+json");
                        string entryPoint = uri + "/breweries/";
                        var response = client.GetAsync(entryPoint).Result;

                        var data = response.Content.ReadAsStringAsync().Result;
                        var result = (JObject)JsonConvert.DeserializeObject(data);
                        var infoberarii = (ClasaBerarii.BerariiRootObject)result;

                        int totalberarii = infoberarii._embedded.brewery.Count();
                        Console.WriteLine("Lista berarii:");
                        for(int i=0; i<totalberarii; i++)
                        {
                            Console.WriteLine((i+1) + " " +infoberarii._embedded.brewery[i].Name);
                        }

                        Console.WriteLine("Informatii despre berarii:");
                        Console.WriteLine("Alege o berarie intre 1 si " + totalberarii+":");
                        Console.WriteLine("");
                        int rezultat = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(infoberarii._embedded.brewery[rezultat - 1].Name);
                        try
                        {
                            Console.WriteLine("Lista berarii: " + rezultat);
                            string apiBeers = uri + infoberarii._embedded.brewery[rezultat - 1]._links.beers.href;
                            var responseApi = client.GetAsync(apiBeers).Result;
                            var dataApi = responseApi.Content.ReadAsStringAsync().Result;
                            var resultApi = (JObject)JsonConvert.DeserializeObject(dataApi);
                            var infobere = (ClasaBeri.RootObject1)resultApi;
                            Console.WriteLine("Sunt " + infobere._embedded.beer.Count() + " beri in beraria " + infoberarii._embedded.brewery[rezultat - 1].Name);
                            Console.WriteLine("Introduceti o berarie intre 1 si " + infobere._embedded.beer.Count() + " despre care doriti sa aflati informatii: ");
                            int BeerId = Int32.Parse(Console.ReadLine());
                            Console.WriteLine(infobere._embedded.beer[BeerId - 1].Name);


                            Console.WriteLine("Alegeti o bere pentru a vedea linkul");
                            int bereSelectata = Int32.Parse(Console.ReadLine());
                            int totalLinks = infobere._links.beer.Count();
                            Console.WriteLine("Aceasta bere are " + totalLinks + " linkuri");
                            Console.WriteLine("Alegeti un link: ");
                            int linkSelectat = Int32.Parse(Console.ReadLine());
                            Console.WriteLine(infobere._links.beer[bereSelectata - 1].href);

                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Nu sunt beri in aceasta berarie");
                        }

         


                        break;
                    case 2:
                        client = new HttpClient();
                        Console.WriteLine("Adaugare bere\n");
                        Console.WriteLine("Numele berii: ");
                        string AddedBeerName = Console.ReadLine();

                        string beer = "{\"Name\":\"" + AddedBeerName + "\"}";
                        var postResponse = client.PostAsJsonAsync("http://datc-rest.azurewebsites.net/beers", beer);

                        break;
                    default:
                        Console.WriteLine("Optiune gresita!");
                        break;

                }

            } while (opt != 0);
        }
    }
}
