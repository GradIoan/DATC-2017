using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class Menu
    {
        public Menu()
        {
           
        }

        public void Show()
        {
            Console.WriteLine("\nChoose an option");
            //display menu
            Console.WriteLine("\n0. Exit");
            Console.WriteLine("\n1. Show all breweries");
            // Console.WriteLine("\n2.Show beers link from a chosen brewery");
            Console.WriteLine("\n2. Show link for a specific brewery");
            Console.WriteLine("\n3.Add new beer");

        }
            
    }
}
