using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class Berarii
    {
        public class BerariiSelf
        {
            public string href { get; set; }

            public BerariiSelf(string href)
            {
                this.href = href;
            }

            public static explicit operator BerariiSelf(JToken token)
            {
                return new BerariiSelf((string)token["href"]);
            }
        }

        public class BerariiBrewery
        {
            public string href { get; set; }

            public BerariiBrewery(string href)
            {
                this.href = href;
            }

            public static explicit operator BerariiBrewery(JToken token)
            {
                return new BerariiBrewery((string)token["href"]);
            }
        }

        public class BerariiLinks
        {
            public BerariiSelf self { get; set; }
            public List<BerariiBrewery> brewery { get; set; }

            public BerariiLinks(BerariiSelf self, List<BerariiBrewery> brewery)
            {
                this.self = self;
                this.brewery = brewery;
            }

            public static explicit operator BerariiLinks(JToken token)
            {
                List<BerariiBrewery> breweryList = token["brewery"].ToObject<List<BerariiBrewery>>();
                return new BerariiLinks((BerariiSelf)token["self"], breweryList);
            }
        }

        public class BerariiSelf2
        {
            public string href { get; set; }

            public BerariiSelf2(string href)
            {
                this.href = href;
            }

            public static explicit operator BerariiSelf2(JToken token)
            {
                return new BerariiSelf2((string)token["href"]);
            }
        }

        public class BerariiBeers
        {
            public string href { get; set; }

            public BerariiBeers(string href)
            {
                this.href = href;
            }

            public static explicit operator BerariiBeers(JToken token)
            {
                return new BerariiBeers((string)token["href"]);
            }
        }

        public class BerariiLinks2
        {
            public BerariiSelf2 self { get; set; }
            public BerariiBeers beers { get; set; }

            public BerariiLinks2(BerariiSelf2 self, BerariiBeers beers)
            {
                this.self = self;
                this.beers = beers;
            }

            public static explicit operator BerariiLinks2(JToken token)
            {
                return new BerariiLinks2((BerariiSelf2)token["self"], (BerariiBeers)token["beers"]);
            }
        }

        public class BerariiBrewery2
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public BerariiLinks2 _links { get; set; }

            public BerariiBrewery2(int Id, string Name, BerariiLinks2 _links)
            {
                this.Id = Id;
                this.Name = Name;
                this._links = _links;
            }

            public static explicit operator BerariiBrewery2(JToken token)
            {
                return new BerariiBrewery2((int)token["Id"], (string)token["Name"],(BerariiLinks2)token["_links"]);
            }
        }

        public class BerariiEmbedded
        {
            public List<BerariiBrewery2> brewery { get; set; }

            public BerariiEmbedded(List<BerariiBrewery2> brewery)
            {
                this.brewery = brewery;
            }

            public static explicit operator BerariiEmbedded(JToken token)
            {
                List<BerariiBrewery2> breweryList = token["brewery"].ToObject<List<BerariiBrewery2>>();
                return new BerariiEmbedded(breweryList);
            }

        }

        public class BerariiRootObject
        {
            public BerariiLinks _links { get; set; }
            public BerariiEmbedded _embedded { get; set; }

            public BerariiRootObject(BerariiLinks _links, BerariiEmbedded _embedded)
            {
                this._links = _links;
                this._embedded = _embedded;
            }

            public static explicit operator BerariiRootObject(JToken token)
            {
                return new BerariiRootObject((BerariiLinks)token["_links"], (BerariiEmbedded)token["_embedded"]);
            }
        }
    }
}
