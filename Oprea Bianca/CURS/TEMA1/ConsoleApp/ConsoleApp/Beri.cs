using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Beri
    {
        public List<resrc> ResourceList { get; set; }
        public class resrc
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public long BreweryId { get; set; }
            public string BreweryName { get; set; }
            public long StyleId { get; set; }
            public string StyleName { get; set; }
            public string links { get; set; }
        }

    }
}
