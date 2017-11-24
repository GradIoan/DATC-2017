using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Tema1_Grama
{
    public class Beers
    {
        public string href { get; set; }
        public int Id;
        public string Name;
        public Beers(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        
    }
}
