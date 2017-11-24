using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hal.Client
{
    public class Self2Beer
    {
        public Self2Beer(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator Self2Beer(JToken v)
        {
            return new Self2Beer((string)v["href"]);
        }
    }

    public class Style
    {
        public Style(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator Style(JToken v)
        {
            return new Style((string)v["href"]);
        }
    }

    public class BreweryBeer
    {
        public BreweryBeer(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator BreweryBeer(JToken v)
        {
            return new BreweryBeer((string)v["href"]);
        }
    }

    public class Links2Beer
    {
        public Links2Beer(Self2Beer self, Style style, BreweryBeer brewery)
        {
            this.self = self;
            this.style = style;
            this.brewery = brewery;
        }

        public Self2Beer self { get; set; }
        public Style style { get; set; }
        public BreweryBeer brewery { get; set; }

        public static explicit operator Links2Beer(JToken v)
        {
            return new Links2Beer((Self2Beer)v["self"], (Style)v["style"], (BreweryBeer)v["brewery"]);
        }
    }

    public class Beer2
    {
        public Beer2(int id, string name, int breweryId, string breweryName, int styleId, string styleName, Links2Beer _links)
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
        public Links2Beer _links { get; set; }

        public static explicit operator Beer2(JToken v)
        {
            return new Beer2((int)v["Id"], (string)v["Name"], (int)v["BreweryId"], (string)v["BreweryName"], (int)v["StyleId"], (string)v["StyleName"], (Links2Beer)v["_links"]);
        }
    }

    public class EmbeddedBeer
    {
        public EmbeddedBeer(List<Beer2> beer)
        {
            this.beer = beer;
        }

        public List<Beer2> beer { get; set; }

        public static explicit operator EmbeddedBeer(JToken v)
        {
            List<Beer2> Beer2List = v["beer"].ToObject<List<Beer2>>();
            return new EmbeddedBeer(Beer2List);
        }
    }

    public class RootObjectBeers
    {
        public RootObjectBeers(int totalResults, int totalPages, int page, EmbeddedBeer _embedded)
        {
            TotalResults = totalResults;
            TotalPages = totalPages;
            Page = page;
            this._embedded = _embedded;
        }

        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public EmbeddedBeer _embedded { get; set; }

        public static explicit operator RootObjectBeers(JObject v)
        {
            return new RootObjectBeers((int)v["TotalResults"], (int)v["TotalPages"], (int)v["Page"], (EmbeddedBeer)v["_embedded"]);
        }
    }

}
