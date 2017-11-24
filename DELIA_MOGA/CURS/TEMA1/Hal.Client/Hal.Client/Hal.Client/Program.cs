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
            Console.WriteLine("=========Meniu=========");
            Console.WriteLine("1) Exploreaza API-ul");
            Console.WriteLine("2) Adauga o noua bere.");
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
                        var response = ApiLogic.CreateRequest("/breweries", "GET", null);
                        var data = response.Content.ReadAsStringAsync().Result;
                        var result = (JObject)JsonConvert.DeserializeObject(data);
                        var berariiInfo = (RootObject)result; // mapare pe clasa RootObject
                        int NumarBerarii = berariiInfo._embedded.brewery.Count();
                        Console.WriteLine("Exista in total informatii pentru " + NumarBerarii + " berarii.");
                        for (int i = 1; i <= NumarBerarii; i++)
                        {
                            Console.WriteLine(i + ") " + berariiInfo._embedded.brewery[i - 1].Name);
                        }
                        Console.WriteLine("Care dintre ele doriti sa o explorati?");
                        Console.WriteLine("Id-ul dorit >");
                        int IdBerarie = Int32.Parse(Console.ReadLine());
                        if (IdBerarie > berariiInfo._embedded.brewery.Count())
                        {
                            Console.WriteLine("Nu exista aceasta berarie");
                            break;
                        }

                        Console.WriteLine();
                        Console.WriteLine("Berarie aleasa: " + berariiInfo._embedded.brewery[IdBerarie - 1].Name);
                        response = ApiLogic.CreateRequest(berariiInfo._embedded.brewery[IdBerarie - 1]._links.beers.href, "GET", null);
                        data = response.Content.ReadAsStringAsync().Result;                       
                        result = (JObject)JsonConvert.DeserializeObject(data);
                        int totalResults = (int)result["TotalResults"];
                        if (totalResults == 0)
                        {
                            Console.WriteLine("Nu exista beri pentru beraria aleasa!");
                            break;
                        }
                        var bereInfo = (RootObjectBeers)result; // mapare pe clasa RootObjectBeers
                        int NumarBeri = bereInfo._embedded.beer.Count();
                        Console.WriteLine("Exista " + NumarBeri + " beri disponibile la beraria "+ berariiInfo._embedded.brewery[IdBerarie - 1].Name);
                        for (int i = 1; i <= NumarBeri; i++)
                        {
                            Console.WriteLine(i + ") " + bereInfo._embedded.beer[i-1].Name);
                        }
                        Console.WriteLine("Care dintre ele doriti sa o explorati?");
                        Console.WriteLine("Id-ul dorit >");
                        int IdBere = Int32.Parse(Console.ReadLine());
                        if (IdBere > bereInfo._embedded.beer.Count())
                        {
                            Console.WriteLine("Nu exista acesta bere!");
                            break;
                        }
                        Console.WriteLine("Nume => " + bereInfo._embedded.beer[IdBere-1].Name);
                        Console.WriteLine("Nume berarie => " + bereInfo._embedded.beer[IdBere - 1].BreweryName);
                        Console.WriteLine("Nume stil => " + bereInfo._embedded.beer[IdBere - 1].StyleName);
                        
                        Console.WriteLine();
                        Console.WriteLine("Descopera berile care au acelasi stil:");
                        response = ApiLogic.CreateRequest(bereInfo._embedded.beer[IdBere-1]._links.style.href + "/beers", "GET", null);
                        data = response.Content.ReadAsStringAsync().Result;
                        result = (JObject)JsonConvert.DeserializeObject(data);
                        totalResults = (int)result["TotalResults"];
                        if (totalResults == 0)
                        {
                            Console.WriteLine("Nu exista beri cu acest stil!");
                            break;
                        }
                        else if(totalResults == 1)
                        {
                            Console.WriteLine("Este singura bere cu acest stil!");
                            break;
                        }
                        var lista_beri = (RootObjectStyles)result; // mapare pe clasa RootObjectStyles
                        foreach (Beer4 bere4 in lista_beri._embedded.beer)
                        {
                            Console.WriteLine("Id: " + bere4.Id+ " Nume: " + bere4.Name);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Ce bere doriti sa adaugati?\n");
                        Console.WriteLine("Denumire bere >");
                        string denumireBereAdaugata = Console.ReadLine();
                        Beer bere = new Beer(denumireBereAdaugata);
                           
                        response = ApiLogic.CreateRequest(null, "POST", bere);
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Berea a fost inregistrata cu succes!");
                        }
                        else
                        {
                            Console.WriteLine("Berea nu a fost adaugata..");
                        }                   
                        break;
                }
            } while (option != 0);
        }
    }
}
