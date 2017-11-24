using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Grama
{
    public class Embedded
    {
        public List<Brewery2> brewery { get; set; }
        public List<Beers> beers { get; set; }

        public void MeniuEmbedded()
        {
            foreach(Brewery2 i in brewery)
            {
                Console.WriteLine("Id= " + i.Id + " Name=" + i.Name);
            }

            Console.WriteLine("Afiseaza berile cu Id: ");
            string selection = Console.ReadLine();
            int sel = Int32.Parse(selection);
            
            Process.Start(Program.getUrl()  + brewery[sel]._links.beers.href);

        }
    }
}
