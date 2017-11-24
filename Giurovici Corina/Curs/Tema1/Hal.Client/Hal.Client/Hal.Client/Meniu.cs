using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class Meniu
    {
        int optiune;

        public int getOptiune()
        {
            Console.WriteLine("Alege optiunea");
            Console.WriteLine("1. Get");
            Console.WriteLine("2. Post");
            optiune = Int32.Parse(Console.ReadLine());
            return optiune;
        }
    }
}
