using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    public class SelfBeer
    {
        public string href { get; set; }
    }

    public class PageBeer
    {
        public string href { get; set; }
        public bool templated { get; set; }
    }

    public class BeerBeer
    {
        public string href { get; set; }
    }

    public class LinksBeer
    {
        public SelfBeer self { get; set; }
        public List<PageBeer> page { get; set; }
        public List<BeerBeer> beer { get; set; }
    }

    public class Self2Beer
    {
        public string href { get; set; }
    }

    public class StyleBeer
    {
        public string href { get; set; }
    }

    public class BreweryBeer
    {
        public string href { get; set; }
    }

    public class Links2Beer
    {
        public Self2 self { get; set; }
        public StyleBeer style { get; set; }
        public Brewery brewery { get; set; }
    }

    public class Beer2Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BreweryId { get; set; }
        public string BreweryName { get; set; }
        public int StyleId { get; set; }
        public string StyleName { get; set; }
        public Links2Beer _links { get; set; }
    }

    public class EmbeddedBeer
    {
        public List<Beer2Beer> beer { get; set; }
    }

    public class RootObjectBeer
    {
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public LinksBeer _links { get; set; }
        public EmbeddedBeer _embedded { get; set; }
    }

}
