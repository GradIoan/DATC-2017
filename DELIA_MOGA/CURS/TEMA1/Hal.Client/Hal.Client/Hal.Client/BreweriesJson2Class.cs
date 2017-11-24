using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hal.Client
{
    public class Self
    {
        public Self(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator Self(JToken v)
        {
            return new Self((string)v["href"]);
        }
    }

    public class Brewery
    {
        public Brewery(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator Brewery(JToken v)
        {
            return new Brewery((string)v["href"]);
        }
    }

    public class Links
    {
        public Links(Self self, List<Brewery> brewery)
        {
            this.self = self;
            this.brewery = brewery;
        }

        public Self self { get; set; }
        public List<Brewery> brewery { get; set; }

        public static explicit operator Links(JToken v)
        {
            List<Brewery> breweryList = v["brewery"].ToObject<List<Brewery>>();
            return new Links((Self)v["self"], breweryList);
        }
    }

    public class Self2
    {
        public Self2(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator Self2(JToken v)
        {
            return new Self2((string)v["href"]);
        }
    }

    public class Beers
    {
        public Beers(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator Beers(JToken v)
        {
            return new Beers((string)v["href"]);
        }
    }

    public class Links2
    {
        public Links2(Self2 self, Beers beers)
        {
            this.self = self;
            this.beers = beers;
        }

        public Self2 self { get; set; }
        public Beers beers { get; set; }

        public static explicit operator Links2(JToken v)
        {
            return new Links2((Self2)v["self"], (Beers)v["beers"]);
        }
    }

    public class Brewery2
    {
        public Brewery2(int id, string name, Links2 _links)
        {
            Id = id;
            Name = name;
            this._links = _links;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Links2 _links { get; set; }

        public static explicit operator Brewery2(JToken v)
        {
            return new Brewery2((int)v["Id"], (string)v["Name"], (Links2)v["_links"]);
        }
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
        }
    }

    public class RootObject
    {
        public RootObject(Links _links, Embedded _embedded)
        {
            this._links = _links;
            this._embedded = _embedded;
        }

        public Links _links { get; set; }
        public Embedded _embedded { get; set; }

        public static explicit operator RootObject(JToken v)
        {
            return new RootObject((Links)v["_links"], (Embedded)v["_embedded"]);
        }
    }
}
