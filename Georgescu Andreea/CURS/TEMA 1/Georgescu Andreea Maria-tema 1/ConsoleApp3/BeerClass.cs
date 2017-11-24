using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
   
        public class BeerList
        {
            public BeerList(string Name, int id)
            {
                this.Id = id;
                this.Name = Name;
            }
            public int Id { get; set; }
            public string Name { get; set; }
            public int BreweryId { get; set; }
            public string BreweryName { get; set; }
            public int StyleId { get; set; }
            public string StyleName { get; set; }
            public List<object> _links { get; set; }
        }

    public class Beerclass
    {
            public int TotalResults { get; set; }
            public int TotalPages { get; set; }
            public int Page { get; set; }
            public List<BeerList> ResourceList { get; set; }
            public List<object> _links { get; set; }
            
    }
    
}
