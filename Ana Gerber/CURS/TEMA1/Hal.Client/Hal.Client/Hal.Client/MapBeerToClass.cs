using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hal.Client
{
    public class BeerSelf
    {
        public BeerSelf(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator BeerSelf(JToken t)
        {
            return new BeerSelf((string)t["href"]);
        }
    }
    public class Next
    {

        public string href { get; set; }
    }

    public class Page
    {
        public string href { get; set; }
        public bool templated { get; set; }
    }

    public class Style
    {
        public Style(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator Style(JToken t)
        {
            return new Style((string)t["href"]);
        }
    }

    public class BreweryBeer
    {
        public BreweryBeer(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator BreweryBeer(JToken t)
        {
            return new BreweryBeer((string)t["href"]);
        }
    }

    public class BeerLinks2
    {
        public BeerLinks2 (BeerSelf self, Style style, BreweryBeer brewery)
        {
            this.self = self;
            this.style = style;
            this.brewery = brewery;
        }

        public BeerSelf self { get; set; }
        public Style style { get; set; }
        public BreweryBeer brewery { get; set; }

        public static explicit operator BeerLinks2(JToken t)
        {
            return new BeerLinks2((BeerSelf)t["self"], (Style)t["style"], (BreweryBeer)t["brewery"]);
        }
    }

    public class Beer2
    {
        public Beer2(int id, string name, int breweryId, string breweryName, int styleId, string styleName, BeerLinks2 _links)
        {
            Id = id;
            Name = name;
            BreweryId = breweryId;
            BreweryName = breweryName;
            StyleId = styleId;
            StyleName = styleName;
            this._links = _links;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int BreweryId { get; set; }
        public string BreweryName { get; set; }
        public int StyleId { get; set; }
        public string StyleName { get; set; }
        public BeerLinks2 _links { get; set; }

        public static explicit operator Beer2(JToken t)
        {
            return new Beer2((int)t["Id"], (string)t["Name"], (int)t["BreweryId"], (string)t["BreweryName"], (int)t["StyleId"], (string)t["StyleName"], (BeerLinks2)t["_links"]);
        }
    }

    public class BeerEmbedded
    {
        public BeerEmbedded(List<Beer2> beer)
        {
            this.beer = beer;
        }

        public List<Beer2> beer { get; set; }

        public static explicit operator BeerEmbedded(JToken t)
        {
            List<Beer2> Beer2List = t["beer"].ToObject<List<Beer2>>();
            return new BeerEmbedded(Beer2List);
        }
    }

    public class RootObjectBeers
    {
        public RootObjectBeers(int totalResults, int totalPages, int page, BeerEmbedded _embedded)
        {
            TotalResults = totalResults;
            TotalPages = totalPages;
            Page = page;
            this._embedded = _embedded;
        }

        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public BeerEmbedded _embedded { get; set; }

        public static explicit operator RootObjectBeers(JObject t)
        {
            return new RootObjectBeers((int)t["TotalResults"], (int)t["TotalPages"], (int)t["Page"], (BeerEmbedded)t["_embedded"]);
        }
    }

}
