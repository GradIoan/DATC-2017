using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class ClasaMeniu
    {
        public int Meniu()
        {
            int opt;
            Console.WriteLine("------------------------------------");
            Console.WriteLine("0. Iesire");
            Console.WriteLine("1. Afisare lista berarii");
            Console.WriteLine("2. Adaugare");
            opt = Int32.Parse(Console.ReadLine());
            return opt;
        }
    }
}
