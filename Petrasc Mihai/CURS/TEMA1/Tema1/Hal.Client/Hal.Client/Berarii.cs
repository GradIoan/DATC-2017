using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hal.Client
{
    //class Berarii
    //{
    //}
    public class Self1
    {
        private string v;

        public Self1(string v)
        {
            this.v = v;
        }

        public string href { get; set; }

        public static explicit operator Self1(JToken v)
        {
            return new Self1((string)v["href"]);
            //throw new NotImplementedException();
        }
    }

    public class Brewery
    {
        public string href { get; set; }
    }

    public class Links1
    {
        private Self1 self1;
        private List<Brewery> breweryList;

        public Links1(Self1 self1, List<Brewery> breweryList)
        {
            this.self1 = self1;
            this.breweryList = breweryList;
        }

        public Self self { get; set; }
        public List<Brewery> brewery { get; set; }

        public static explicit operator Links1(JToken v)
        {
            List<Brewery> breweryList = v["brewery"].ToObject<List<Brewery>>();
            return new Links1((Self1)v["self"], breweryList);
            //throw new NotImplementedException();
        }
    }

    public class Self21
    {
        public string href { get; set; }
    }

    public class Beers
    {
        public string href { get; set; }
    }

    public class Links21
    {
        public Self2 self { get; set; }
        public Beers beers { get; set; }
    }

    public class Brewery2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Links2 _links { get; set; }
    }

    public class Embedded
    {
      
        public Embedded(List<Brewery2> brewery)
        {
            this.brewery = brewery;
        }

        public List<Brewery2> brewery { get; set; }

        public static explicit operator Embedded(JToken v)
        {
            List<Brewery2> breweryList = v["brewery"].ToObject<List<Brewery2>>();
            return new Embedded(breweryList);
            //throw new NotImplementedException();
        }
    }

    public class RootObject1
    {

        public RootObject1(Links1 _links, Embedded _embedded)
        {
            this._links = _links;
            this._embedded = _embedded;
        }

        public Links1 _links { get; set; }
        public Embedded _embedded { get; set; }

        public static explicit operator RootObject1(JObject v)
        {
            //throw new NotImplementedException();
            return new RootObject1((Links1)v["_links"], (Embedded)v["_embedded"]);
        }
    }
}
