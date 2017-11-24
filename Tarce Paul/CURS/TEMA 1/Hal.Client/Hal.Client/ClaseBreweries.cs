using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class ClaseBreweries
    {
    }
    //CLASELE CARE LA CARE MAPEZ JSON-UL RETURNAT DE "http://datc-rest.azurewebsites.net/breweries"

    public class SelfB
    {
        public string href { get; set; }
    }

    public class BreweryB
    {
        public string href { get; set; }
    }

    public class LinksB
    {
        public SelfB self { get; set; }
        public List<BreweryB> brewery { get; set; }
    }

    public class BeersB
    {
        public string href { get; set; }
    }

    public class Links2B
    {
        public SelfB self { get; set; }
        public BeersB beers { get; set; }
    }

    public class Brewery2B
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public Links2B _links { get; set; }
    }

    public class EmbeddedB
    {
        public List<Brewery2B> brewery { get; set; }
    }

    public class RootObjectB
    {
        public LinksB _links { get; set; }
        public EmbeddedB _embedded { get; set; }
    }
}
