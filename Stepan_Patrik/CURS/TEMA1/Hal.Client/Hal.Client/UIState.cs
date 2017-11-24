using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    enum UIStateType
    {
        MAIN_MENU,
        LIST_ALL_BREWERIES,
        LIST_BEERS_PER_BREWERY,
        BEER_DETAILS,
        LIST_ALL_BEERS,
        SEARCH_BEER_BY_NAME,
        ADD_BEER,
        LIST_ALL_STYLES,
        ERROR
    }

    class Service
    {
        public static HttpClient httpClient;

        static Service()
        {
            httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri("http://datc-rest.azurewebsites.net/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/hal+json");
        }
    }

    class UIMainMenuState
    {
        static private UIStateType[] CmdToState = {
            UIStateType.LIST_ALL_BREWERIES,
            UIStateType.LIST_ALL_BEERS,
            UIStateType.ADD_BEER,
           // UIStateType.LIST_ALL_STYLES
        };
        

        public static UIStateType Execute()
        {
            UIStateType nextState = UIStateType.MAIN_MENU;

            Print();
            int cmd = ReadCommand();
            if(cmd >= 0)
                nextState = CmdToState[cmd];

            return nextState;
        }

        private static void Print()
        {
            Console.Clear();
            Console.WriteLine("--------------------> MAIN MENU <---------------------");
            Console.WriteLine("0. List all breweries");
            Console.WriteLine("1. List all beers");
            //Console.WriteLine("2. Search beer by name");
            Console.WriteLine("2. Add beer");
           // Console.WriteLine("3. List all styles");

            Console.WriteLine("\n command>:");
        }

        private static int ReadCommand()
        {
            // Remain in Main Menu View if the string input cannot be parsed
            int numCmd;
            int nextState = -1;

            string cmd = Console.ReadLine();
            if (Int32.TryParse(cmd, out numCmd) == true)
            {
                if (numCmd >= 0 && numCmd < 5)
                    nextState = numCmd;
            }

            return nextState;
        }
    }

    class UIBreweriesListState
    {
        public static List<Brewery> gBreweries;
        public static int currentBrewery;

        public static UIStateType Execute()
        {
            UIStateType nextState = UIStateType.LIST_ALL_BREWERIES;

            gBreweries = Brewery.GetBrewriesRequest(Service.httpClient);
            Brewery.PrintList(gBreweries);

            Console.WriteLine("Type '..' to go back!");
            Console.Write("Command (shoud be an integer between 0 and {0}): ", gBreweries.Count - 1);
            string command = Console.ReadLine();
            if (Int32.TryParse(command, out currentBrewery) == true)
            {
                if (currentBrewery >= 0 && currentBrewery < gBreweries.Count)
                    nextState = UIStateType.LIST_BEERS_PER_BREWERY;
                else
                {
                    Console.WriteLine("\nInvalid input!\n");
                    Console.ReadLine();
                }
            }
            else
            {
                if (command == "..")
                    nextState = UIStateType.MAIN_MENU; // going back
                else
                {
                    Console.WriteLine("\nInvalid input!\n");
                    Console.ReadLine();
                }
            }

            return nextState;
        }
    }


    class UIBeersPerBrewery
    {
        public static List<Beer> gBeers;
        public static int currentBeer;
        public static bool active = false;

        public static UIStateType Execute()
        {
            UIStateType nextState = UIStateType.LIST_BEERS_PER_BREWERY;

            // currentBrewery should be a valid index here
            // obtain the link for the requested resource
            var nextLink = UIBreweriesListState.gBreweries[UIBreweriesListState.currentBrewery].links.beers.href;

            gBeers = Beer.GetBeersRequest(Service.httpClient, nextLink);

            if (gBeers != null)
                Beer.PrintList(gBeers);
            else
            {
                active = false;
                nextState = UIStateType.LIST_ALL_BREWERIES;
                Console.WriteLine("Resource unavailable!\n");
                Console.ReadLine();
                return nextState;
            }

            Console.WriteLine("Type '..' to go back!");
            Console.Write("Type the index of the beer in order too get more details about it: ");
            string command = Console.ReadLine();
            if (Int32.TryParse(command, out currentBeer) == true)
            {
                nextState = UIStateType.BEER_DETAILS;
                active = true;
            }
            else
            {
                if (command == "..")
                {
                    nextState = UIStateType.LIST_ALL_BREWERIES; // going back  
                }
                else
                {
                    Console.WriteLine("Wrong index!");
                    Console.ReadLine();
                }
                active = false;
            }

            return nextState;
        }
    }

    class UIBeerDetails
    {
        public static UIStateType Execute()
        {
            UIStateType nextState = UIStateType.ERROR;
            Beer.PrintDetails(UIBeersPerBrewery.gBeers[UIBeersPerBrewery.currentBeer]);

            if(UIBeersPerBrewery.active)
                nextState = UIStateType.LIST_BEERS_PER_BREWERY;
            else
                nextState = UIStateType.LIST_ALL_BEERS;

            Console.WriteLine("Hit any key to go back!\n");
            Console.ReadLine();

            return nextState;
        }
    }

    class UIBeerList
    {
        static long currentPage = 1;
        static string searchTerm = "";

        public static UIStateType Execute()
        {
            UIStateType nextState = UIStateType.LIST_ALL_BEERS;

            long totalPages, totalResults;
            UIBeersPerBrewery.gBeers = Beer.GetBeersRequest(Service.httpClient, "/beers", currentPage, searchTerm,
                out currentPage, out totalPages, out totalResults);
            Beer.PrintList(UIBeersPerBrewery.gBeers);
            Console.WriteLine("Current page: {0}\nTotal pages {1}\nTotal results {2}",
                currentPage, totalPages, totalResults);

            Console.WriteLine("Type \'..\' to go back!");
            Console.WriteLine("Type \'page n\' to view page \'n\'");
            //Console.WriteLine("Type 'name' to  search for beer name");
            Console.WriteLine("Type a number between 0 and {0} to view beer details", UIBeersPerBrewery.gBeers.Count);
            string cmd = Console.ReadLine();
            if (cmd == "..")
            {
                currentPage = 1;
                searchTerm = "";
                return UIStateType.MAIN_MENU;
            }
            if(cmd.Contains("page") && cmd.Length > 5)
            {
                string subcmd = cmd.Substring(5, cmd.Length - 5);
                int page;
                if (Int32.TryParse(subcmd, out page) == true)
                {
                    currentPage = page;
                    return UIStateType.LIST_ALL_BEERS;
                }
            }

           int beer;
           if (Int32.TryParse(cmd,out beer) == true  && beer < UIBeersPerBrewery.gBeers.Count)
           {
               UIBeersPerBrewery.currentBeer = beer;
               return UIStateType.BEER_DETAILS;
           }

           if(cmd.Contains("name") && cmd.Length > 5)
            {
                searchTerm = cmd.Substring(5, cmd.Length - 5);
                return UIStateType.LIST_ALL_BEERS;
            }

            Console.WriteLine("Wrong format!");
            Console.ReadLine();

            return nextState; // Allways return to main menu
        }
    }

    class UIAddBeer
    {
        public static UIStateType Execute()
        {
            Console.Clear();
            Console.WriteLine("Type \'..\' to go back!");
            Console.WriteLine("Beer name: ");
            string name= Console.ReadLine();
            if (name == "..")
                return UIStateType.MAIN_MENU;

            if(Beer.AddBeer(Service.httpClient,name) == true)
                return UIStateType.MAIN_MENU;
            else
            {
                Console.WriteLine("Eroare!");
                Console.ReadLine();
                return UIStateType.ADD_BEER;
            }
        }
    }
}
