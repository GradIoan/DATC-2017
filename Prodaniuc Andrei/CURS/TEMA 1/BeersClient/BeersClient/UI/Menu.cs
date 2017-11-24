using BeersClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeersClient.UI
{
    public static class Menu
    {
        public static void Display(List<Brewery> breweries)
        {
            Console.Clear();    
            foreach(var brewery in breweries)
            {
                Console.WriteLine(brewery.Id + " - " + brewery.Name +
                    " - " + brewery.Links.Self.Href);
            }
            Console.WriteLine("\nq-Quit");
        }

        internal static void Display(Brewery brewery)
        {
            Console.Clear();
            Console.WriteLine(brewery.Id + " - " + brewery.Name +
                " - " +brewery.Links.Self.Href);
            Console.WriteLine("Beers - "+ brewery.Links.Beers.Href);
            Console.WriteLine("\nbeers");
            Console.WriteLine("b-Back");
            Console.WriteLine("q-Quit");

        }

        internal static void Display(List<Beer> list)
        {
            Console.Clear();
            foreach(var beer in list)
            {
                Console.WriteLine(beer.Id + " - " + beer.Name);
            }
            Console.WriteLine("\nb-Back");
            Console.WriteLine("q-Quis");
        }

        public static void Display(Beer beer)
        {

        }
    }
}
