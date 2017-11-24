using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaDATC
{
    class ShowData
    {
        public static void Show_menu()
        {
            Console.WriteLine("1 - Show Breweries");
            Console.WriteLine("2 - Show ALL Beers");
            Console.WriteLine("3 - Show Beers By Brewery");
            Console.WriteLine("4 - Add New Beer");
            Console.WriteLine("0 - Exit");
        }

        public static void Show_Breweries()
        {
            Console.WriteLine(string.Join(Environment.NewLine, MyList.Breweries_List.Select(brewery => brewery._id.ToString().PadRight(5) + brewery._name)));
        }

        public static void Show_Beers()
        {
            int max = MyList.Beers_List.Max(beer => beer._brewery.Length) + 10;
            Console.WriteLine(string.Join(Environment.NewLine, MyList.Beers_List.Select(beer => beer._brewery.PadRight(max) + beer._beer)));
        }

        public static void Show_Beers_by_Breweries(string brewery)
        {
            int max = MyList.Beers_List.Max(beer => beer._brewery.Length) + 10;
            foreach (Beers my_beers in MyList.Beers_List)
            {
                if(my_beers._brewery.Contains(brewery))
                {
                    Console.WriteLine(string.Join(Environment.NewLine, my_beers._brewery.PadRight(max)+my_beers._beer));
                }
            }
            
        }

       
    }
}
