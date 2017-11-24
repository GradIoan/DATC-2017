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
        public static List<Brewery> breweriesList;
        public static List<Beer> beersList;

        static void Main(string[] args)
		{
			

            var brewerys = new GetBreweries("http://datc-rest.azurewebsites.net/breweries");

            int option;
            do
            {
                var menu = new Menu();
                menu.Show();


                option = Convert.ToInt32(Console.ReadLine());


                switch (option)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine("\nBREWERIES");
                        for (int i = 0; i < breweriesList.Count; i++)
                            Console.WriteLine(breweriesList[i].toString());
                        break;

                    case 2:
                        Console.WriteLine("Enter brewery name: ");
                        string b_name = Console.ReadLine();

                        for (int i = 0; i < breweriesList.Count; i++)
                            if(breweriesList[i].Name.Equals(b_name))
                                Console.WriteLine(breweriesList[i].links.self.toString());

                        break;
                    case 3:

                        var beer = new Beer();
                        //string href = "/ beers";
                        Console.WriteLine("Enter ID: "); beer.ID = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("Enter Name: "); beer.Name = Console.ReadLine();
                        //beer.links.self.href = "/beers" + beer.ID;

                        beer.PostBeer();

                        break;

                }
            } while (option != 0);

            Console.ReadLine();
        }
	}
}
