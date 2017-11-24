using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class Program
    {
        static void GetBeers(HttpClient client, string currentPath)
        {
            var response = client.GetAsync(currentPath).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<CBeers>(data);
            

            if (obj.beersEmbedded != null)
            {
                List<CBeer> beers = obj.beersEmbedded.beer;
                foreach (CBeer beer in beers)
                {
                    Console.WriteLine("Id:" + " " + beer.beerId + " " + "Name:" + " " + beer.beerName + " " + "Brewery Id:" + " " + beer.breweryId + " "
                         + "Brewery Name" + " " + beer.breweryName + " " + "Style Id:" + " " + beer.styleId + " " + "Style name:" + " " + beer.styleName);
                }
            }
            else
            {
                Console.WriteLine("Aceasta berarie nu are beri!");
            }

            Console.ReadLine();
        }

        static void GetBrewery(HttpClient client, string optiune)
        {
            var response = client.GetAsync("breweries").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<JSONResources>(data);
            List<CBrewery> breweries = obj.homeEmbedded.brewery.ToList();

            int brwId = Int32.Parse(optiune);
            int ok = 0;
            brwId -= 1;
            if (brwId != 0)
            {
                foreach (CBrewery brw in breweries)
                {
                    if (brw.breweryId.Equals(brwId))
                    {
                        ok = 1;
                    }
                }

                if (ok == 0)
                {
                    Console.WriteLine("Nu exista beraria cu Id-ul:" + brwId);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ati selectat beraria cu id-ul:" + (brwId + 1));
                    Console.WriteLine();
                    Console.WriteLine(breweries[brwId].breweryId + " " + breweries[brwId].breweryName.ToString());
                }
            }
            else
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("In continuare:");
            Console.WriteLine("Doriti sa vizualizati berile acestei berarii? Daca da apasati 1");
            Console.WriteLine("Daca nu, atunci apasati 0 pentru a merge inapoi");
            Console.WriteLine("Optiunea dvs:");

            string option;           
            option = Console.ReadLine();

            if(option == "1")
            {
                brwId += 1;
               var  newPath = breweries[brwId].breweryLinks.beers.hrefBrewerywBeers;
                GetBeers(client, newPath);
            }
            else
            {
                return;
            }
        }

        static void GetBreweries(HttpClient client)
        {
            
            var response = client.GetAsync("breweries").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<JSONResources>(data);                        
            List<CBrewery> breweries = obj.homeEmbedded.brewery.ToList();
            
            Console.Clear();
            Console.WriteLine("-----------Berarii----------");
            foreach (CBrewery brw in breweries)
            {
                Console.WriteLine(brw.breweryId.ToString() + " " + brw.breweryName);
            }

            Console.WriteLine();
            Console.WriteLine("-----------------------------");
            Console.WriteLine("In continuare:");
            Console.WriteLine("Doriti sa vizualizati o berarie? Introduceti id-ul berariei");
            Console.WriteLine("Daca nu apasati 0 pentru a merge inapoi");
            Console.WriteLine("Optiunea dvs:");

            string optiune;
            optiune = Console.ReadLine();

            if(optiune != "0")
            {
                GetBrewery(client, optiune);
            }
            else
            {
                return;
            }
        }

        static void GetAllBeers(HttpClient client, string page)
        {
            //berea mea cu numele "vbbn" se afla la pagina 57
            var response = client.GetAsync("beers" + "?" + "page=" + page).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<CAllBeers>(data);
            
            List<CAllBeer> allBeers = obj.embedded.beer;

            Console.WriteLine("Current page=" + obj.page);
            Console.WriteLine("Total pages=" + obj.totalPages);
            Console.WriteLine("Total results=" + obj.totalResults);
            Console.WriteLine();

            foreach (CAllBeer beer in allBeers)
            {
                 Console.WriteLine(beer.allBeerId + " " + beer.allBeerName);
            }

            Console.ReadLine();
        }

        static void AddBeer(HttpClient client, string beerName)
        {
            //nu am reusit sa fac cautare dupa o bere dar am gasit berea "vbbn" in fiddler
            string obj = JsonConvert.SerializeObject(new { Name = beerName });
            HttpContent content = new StringContent(obj, Encoding.UTF8, "application/hal+json");
            var httpResponse = client.PostAsync("/beers", content);
            Console.ReadLine();
        }
         
        static void Main(string[] args)
        {            
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://datc-rest.azurewebsites.net/");
            client.DefaultRequestHeaders.Add("Accept", "Application/hal+json");

            string option;
            do
            {
                Console.Clear();
                Console.WriteLine("-----------Meniu----------");
                Console.WriteLine("1. Afiseaza toate berariile");
                Console.WriteLine("2. Afiseaza toate berile de pe o pagina");
                Console.WriteLine("3. Adauga bere");
                Console.WriteLine("Alege optiunea:");

               option = Console.ReadLine();

                switch (option)
                {
                    case "1":                       
                        GetBreweries(client);
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Dati numarul paginii");
                        string page = Console.ReadLine();
                        GetAllBeers(client, page);
                        break;
                    case "3":
                        string beerName;
                        Console.WriteLine("Dati numele berii:");
                        beerName = Console.ReadLine();
                        AddBeer(client, beerName); 
                        break;
                    default:
                        Console.WriteLine("Ati introdus o optiune gresita!");
                        break;
                        
                }

            } while (option != "0");

            Console.ReadLine();
        }
    }
}
