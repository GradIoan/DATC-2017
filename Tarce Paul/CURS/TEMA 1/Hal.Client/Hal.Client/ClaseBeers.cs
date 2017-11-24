using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class ClaseBeers
    {
    }

    //CLASELE CARE LA CARE MAPEZ JSON-UL RETURNAT DE "http://datc-rest.azurewebsites.net/breweries/{id}/beers" 
    public class Self
{
    public string href { get; set; }
}

public class Page
{
    public string href { get; set; }
    public bool templated { get; set; }
}

public class Beer
{
    public string href { get; set; }
}

public class Links 
{
    public Self self { get; set; }
    public List<Page> page { get; set; }
    public dynamic beer { get; set; }  //in caz ca am 1 obiect sau mai multe
}

public class Self2
{
    public string href { get; set; }
}

public class Style
{
    public string href { get; set; }
}

public class Brewery
{
    public string href { get; set; }
}

public class Links2
{
    public Self2 self { get; set; }
    public Style style { get; set; }
    public Brewery brewery { get; set; }
}

public class Beer2
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BreweryId { get; set; }
    public string BreweryName { get; set; }
    public int StyleId { get; set; }
    public string StyleName { get; set; }
    public Links2 _links { get; set; }

        public override string  ToString()
        {
            return "\nId:"+Id + "\nName:" + Name + "\nBreweryId:" + BreweryId + "\nBreweryName:" + BreweryName + "\nStyleId:" + StyleId + "\nStyleName:" + StyleName;
        }
}

public class Embedded
{
    public List<Beer2> beer { get; set; }
}

public class RootObject
{
    public int TotalResults { get; set; }
    public int TotalPages { get; set; }
    public int Page { get; set; }
    public Links _links { get; set; }
    public Embedded _embedded { get; set; }
}

}
