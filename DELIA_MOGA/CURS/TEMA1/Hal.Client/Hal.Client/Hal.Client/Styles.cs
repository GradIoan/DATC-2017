using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hal.Client
{
    public class Self4
    {
        public Self4(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator Self4(JToken v)
        {
            return new Self4((string)v["href"]);
        }
    }

    public class Style4
    {
        public Style4(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator Style4(JToken v)
        {
            return new Style4((string)v["href"]);
        }
    }

    public class Brewery4
    {
        public Brewery4(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator Brewery4(JToken v)
        {
            return new Brewery4((string)v["href"]);
        }
    }

    public class Links4
    {
        public Links4(Self4 self, Style4 style, Brewery4 brewery)
        {
            this.self = self;
            this.style = style;
            this.brewery = brewery;
        }

        public Self4 self { get; set; }
        public Style4 style { get; set; }
        public Brewery4 brewery { get; set; }

        public static explicit operator Links4(JToken v)
        {
            return new Links4((Self4)v["self"], (Style4)v["style"], (Brewery4)v["brewery"]);
        }
    }

    public class Beer4
    {
        public Beer4(int id, string name, int breweryId, string breweryName, int styleId, string styleName, Links4 links)
        {
            Id = id;
            Name = name;
            BreweryId = breweryId;
            BreweryName = breweryName;
            StyleId = styleId;
            StyleName = styleName;
            _links = links;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int BreweryId { get; set; }
        public string BreweryName { get; set; }
        public int StyleId { get; set; }
        public string StyleName { get; set; }
        public Links4 _links { get; set; }

        public static explicit operator Beer4(JToken v)
        {
            return new Beer4((int)v["Id"], (string)v["Name"], (int)v["BreweryId"], (string)v["BreweryName"], (int)v["StyleId"], (string)v["StyleName"], (Links4)v["_links"]);
        }
    }

    public class Embedded4
    {
        public Embedded4(List<Beer4> beer)
        {
            this.beer = beer;
        }

        public List<Beer4> beer { get; set; }

        public static explicit operator Embedded4(JToken v)
        {
            List<Beer4> breweryList = v["beer"].ToObject<List<Beer4>>();
            return new Embedded4(breweryList);
        }
    }

    public class RootObjectStyles
    {
        public RootObjectStyles(int totalResults, int totalPages, int page, Embedded4 embedded)
        {
            TotalResults = totalResults;
            TotalPages = totalPages;
            Page = page;
            _embedded = embedded;
        }

        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public Embedded4 _embedded { get; set; }

        public static explicit operator RootObjectStyles(JObject v)
        {
            return new RootObjectStyles((int)v["TotalResults"], (int)v["TotalPages"], (int)v["Page"], (Embedded4)v["_embedded"]);
        }
    }
}
