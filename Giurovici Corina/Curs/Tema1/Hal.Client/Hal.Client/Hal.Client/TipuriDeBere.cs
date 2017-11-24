using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class TipuriDeBere
    {
        public class Self
        {
            public string href { get; set; }

            public Self(string href)
            {
                this.href = href;
            }

            public static explicit operator Self(JToken token)
            {
                return new Self((string)token["href"]);
            }
        }

        public class Next
        {
            public string href { get; set; }

            public Next(string href)
            {
                this.href = href;
            }

        //    public static explicit operator Next(JToken token)
      //      {
        //        return new Next((string)token["href"]);
         //   }
        }

        public class Page
        {
            public string href { get; set; }
            public bool templated { get; set; }

            public Page(string href, bool templated)
            {
                this.href = href;
                this.templated = templated;
            }

            public static explicit operator Page(JToken token)
            {
                return new Page((string)token["href"],(bool)token["templated"]);
            }
        }

        public class Beer
        {
            public string href { get; set; }

            public Beer(string href)
            {
                this.href = href;
            }

            public static explicit operator Beer(JToken token)
            {
                return new Beer((string)token["href"]);
            }
        }

        public class Links
        {
            public Self self { get; set; }
           // public Next next { get; set; }
            public List<Page> page { get; set; }
            public List<Beer> beer { get; set; }

            public Links(Self self, List<Page> page, List<Beer> beer)
            {
                this.self = self;
              //  this.next = next;
                this.page = page;
                this.beer = beer;
            }

            public static explicit operator Links(JToken token)
            {
                List<Page> pageList = token["page"].ToObject<List<Page>>();
                List<Beer> beerList = token["beer"].ToObject<List<Beer>>();
                return new Links((Self)token["self"], pageList, beerList);
            }

        }

        public class Self2
        {
            public string href { get; set; }

            public Self2(string href)
            {
                this.href = href;
            }

            public static explicit operator Self2(JToken token)
            {
                return new Self2((string)token["href"]);
            }
        }

        public class Style
        {
            public string href { get; set; }

            public Style(string href)
            {
                this.href = href;
            }

            public static explicit operator Style(JToken token)
            {
                return new Style((string)token["href"]);
            }
        }

        public class Brewery
        {
            public string href { get; set; }

            public Brewery(string href)
            {
                this.href = href;
            }

            public static explicit operator Brewery(JToken token)
            {
                return new Brewery((string)token["href"]);
            }

        }

        public class Links2
        {
            public Self2 self { get; set; }
            public Style style { get; set; }
            public Brewery brewery { get; set; }

            public Links2(Self2 self, Style style, Brewery brewery)
            {
                this.self = self;
                this.style = style;
                this.brewery = brewery;
            }

            public static explicit operator Links2(JToken token)
            {
                return new Links2((Self2)token["self"], (Style)token["style"], (Brewery)token["brewery"]);
            }
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

            public Beer2(int Id, string Name, int BreweryId, string BreweryName, int StyleId, string StyleName, Links2 _links)
            {
                this.Id = Id;
                this.Name = Name;
                this.BreweryId = BreweryId;
                this.BreweryName = BreweryName;
                this.StyleId = StyleId;
                this.StyleName = StyleName;
                this._links = _links;
            }

            public static explicit operator Beer2(JToken token)
            {
                return new Beer2((int)token["id"], (string)token["Name"], (int)token["BreweryId"], (string)token["BreweryName"], (int)token["StyleId"], (string)token["StyleName"], (Links2)token["_links"]);
            }
        }

        public class Embedded
        {
            public List<Beer2> beer { get; set; }

            public Embedded(List<Beer2> beer)
            {
                this.beer = beer;
            }

            public static explicit operator Embedded(JToken token)
            {
                List<Beer2> beer2List = token["beer"].ToObject<List<Beer2>>();
                return new Embedded(beer2List);
            }
        }

        public class RootObject
        {
            public int TotalResults { get; set; }
            public int TotalPages { get; set; }
            public int Page { get; set; }
            public Links _links { get; set; }
            public Embedded _embedded { get; set; }

            public RootObject(int TotalResults, int TotalPages, int Page, Links _links, Embedded _embedded)
            {
                this.TotalResults = TotalResults;
                this.TotalPages = TotalPages;
                this.Page = Page;
                this._links = _links;
                this._embedded = _embedded;
            }

            public static explicit operator RootObject(JToken token)
            {
                return new RootObject((int)token["TotalResults"], (int)token["TotalPages"], (int)token["Page"], (Links)token["_links"], (Embedded)token["_embedded"]);
            }
        }
    }
}
