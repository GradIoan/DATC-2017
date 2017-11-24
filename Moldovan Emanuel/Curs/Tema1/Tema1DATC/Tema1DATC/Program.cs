using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema1DATC.Models;

namespace Tema1DATC
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BreweryModel> berariiList = new List<BreweryModel>();
            List<BeerModel> beriList = new List<BeerModel>();
            string berarieAleasa;
            string optiune;
            bool exit = false;
            Breweries brewery = new Breweries();
            brewery.GetBreweries();            

            Console.WriteLine("Bine ai venit in portalul nostru");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Doriti sa vizualizati toate berariile asociate?(Y/N)");
            optiune = Console.ReadLine();
            while (!exit)
            {                

                switch (optiune)
                {
                    case "1":
                    case "y":
                    case "Y":
                        Console.WriteLine("Mai jos puteti vedea toate berariile asociate");
                        Console.WriteLine("--------------------------------");

                        int i = 0;
                        int j = 0;
                        foreach (var brew in brewery.breweries._links.brewery)
                        {
                            i++;
                            var berarie = new Breweries();
                            berarie.GetBrewery(brew.href);
                            berariiList.Add(berarie.brewery);
                            if (berarie.brewery.Name != null)
                                Console.WriteLine(i + " - " + berarie.brewery.Name);
                        }
                        Console.WriteLine("Alege beraria pe care vrei sa o vizualizezi");
                        Console.WriteLine("Apasa 0 pentru a iesi");
                        Console.WriteLine("--------------------------------");
                        berarieAleasa = Console.ReadLine();
                        if (!char.IsDigit(Convert.ToChar(berarieAleasa)))
                        {
                            if(berarieAleasa == "n" || berarieAleasa == "N" || Convert.ToInt32(berarieAleasa) == 0)
                            {
                                exit = true;
                                break;
                            }
                            else
                            {
                                break;
                            }

                        }
                        else
                        {
                            if (Convert.ToInt32(berarieAleasa) == 0)
                            {
                                exit = true;
                                break;
                            }
                        }
                        var d = berariiList[Convert.ToInt32(berarieAleasa) - 1]._links.beers.href;
                        brewery.GetBeers(d);

                        foreach (var beer in brewery.beers._links.beer)
                        {
                            j++;
                            var bere = new Breweries();
                            bere.GetBeer(beer.href);
                            beriList.Add(bere.beer);
                            if (bere.beer.Name != null)
                            {
                                Console.WriteLine("________________________________");
                                Console.WriteLine("     " + j + ". Denumire Bere");
                                Console.WriteLine("         " + bere.beer.Name);
                                Console.WriteLine("     Style Bere");
                                Console.WriteLine("         " + bere.beer.StyleName);
                                Console.WriteLine("     Brewery Name");
                                Console.WriteLine("         " + bere.beer.BreweryName);
                                Console.WriteLine("_________________________________");
                            }

                        }
                        Console.WriteLine("1.Navigheaza inapoi la berarii?");
                        Console.WriteLine("2.Adauga o bere");
                        Console.WriteLine("3.Iesire");
                        Console.WriteLine("--------------------------------");
                        optiune = Console.ReadLine();
                        break;
                    case "3":
                    case "0":
                    case "n":
                    case "N":
                        exit = true;
                        break;
                    case "2":
                        Console.WriteLine("Scrie numele berii pe care vrei sa o adaugi");
                        var numeBere = Console.ReadLine();
                        if(WebRequest.Post("/beers", numeBere))
                        {
                            Console.WriteLine("Berea a fost adaugata");
                        }
                        else
                        {
                            Console.WriteLine("Berea nu a putut fi adaugata");
                        }
                        Console.WriteLine("1.Navigheaza inapoi la berarii?");
                        Console.WriteLine("2.Adauga o bere");
                        Console.WriteLine("--------------------------------");
                        optiune = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Optiune neexistent. Introdu alta optiune");
                        Console.WriteLine("--------------------------------");
                        optiune = Console.ReadLine();
                        break;
                }
            }
            
        }
    }
}