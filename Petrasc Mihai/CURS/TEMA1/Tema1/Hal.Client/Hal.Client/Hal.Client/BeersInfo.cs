using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hal.Client
{
    public class Self3
    {
        private string v;

        public Self3(string v)
        {
            this.v = v;
        }

        public string href { get; set; }

        public static explicit operator Self3(JToken v)
        {
            return new Self3((string)v["href"]);
        }
    }

    public class Style3
    {
        private string v;

        public Style3(string v)
        {
            this.v = v;
        }

        public string href { get; set; }

        public static explicit operator Style3(JToken v)
        {
            return new Style3((string)v["href"]);
        }
    }

    public class Brewery3
    {
        private string v;

        public Brewery3(string v)
        {
            this.v = v;
        }

        public string href { get; set; }

        public static explicit operator Brewery3(JToken v)
        {
            return new Brewery3((string)v["href"]);
        }
    }

    public class Review
    {
        public Review(string href)
        {
            this.href = href;
        }

        public string href { get; set; }

        public static explicit operator Review(JToken v)
        {
            return new Review((string)v["href"]);
        }
    }

    public class Links3
    {
        public Links3(Self3 self, Style3 style, Brewery3 brewery, Review review)
        {
            this.self = self;
            this.style = style;
            this.brewery = brewery;
            this.review = review;
        }

        public Self3 self { get; set; }
        public Style3 style { get; set; }
        public Brewery3 brewery { get; set; }
        public Review review { get; set; }

        public static explicit operator Links3(JToken v)
        {

            return new Links3((Self3)v["self"], (Style3)v["style"], (Brewery3)v["brewery"], (Review)v["review"]);
        }
    }

    public class Beer
    {
        public Beer(int id, string name, int breweryId, string breweryName, int styleId, string styleName, Links3 _links)
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
        public Links3 _links { get; set; }

        public static explicit operator Beer(JObject v)
        {
            return new Beer((int)v["Id"], (string)v["Name"], (int)v["BreweryId"], (string)v["BreweryName"], (int)v["StyleId"], (string)v["StyleName"], (Links3)v["_links"]);
        }
    }
}
