using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class ClasaBerarii
    {
        public class BerariiSelf
        {
            public string href { get; set; }
            public BerariiSelf(string href)
            {
                this.href = href;
            }
            public static explicit operator BerariiSelf(JToken jtoken)
            {
                return new BerariiSelf((string)jtoken["href"]);
            }
        }

        public class BerariiBrewery
        {
            public string href { get; set; }
            public BerariiBrewery(string href)
            {
                this.href = href;
            }
            public static explicit operator BerariiBrewery(JToken jtoken)
            {
                return new BerariiBrewery((string)jtoken["href"]);
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
            public static explicit operator BerariiLinks(JToken jtoken)
            {
                List<BerariiBrewery> breweryList = jtoken["brewery"].ToObject<List<BerariiBrewery>>();
                return new BerariiLinks((BerariiSelf)jtoken["self"], breweryList);
            }

        }

        public class BerariiSelf2
        {
            public string href { get; set; }

            public BerariiSelf2(string href)
            {
                this.href = href;
            }
            public static explicit operator BerariiSelf2(JToken jtoken)
            {
                return new BerariiSelf2((string)jtoken["href"]);
            }

        }

        public class BerariiBeers
        {
            public string href { get; set; }
            public BerariiBeers(string href)
            {
                this.href = href;
            }
            public static explicit operator BerariiBeers(JToken jtoken)
            {
                return new BerariiBeers((string)jtoken["href"]);
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
            public static explicit operator BerariiLinks2(JToken jtoken)
            {
                return new BerariiLinks2((BerariiSelf2)jtoken["self"], (BerariiBeers)jtoken["beers"]);
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
            public static explicit operator BerariiBrewery2(JToken jtoken)
            {
                return new BerariiBrewery2((int)jtoken["Id"], (string)jtoken["Name"], (BerariiLinks2)jtoken["_links"]);
            }
        }

        public class BerariiEmbedded
        {
            public List<BerariiBrewery2> brewery { get; set; }
            public BerariiEmbedded(List<BerariiBrewery2> brewery)
            {
                this.brewery = brewery;
            }
            public static explicit operator BerariiEmbedded(JToken jtoken)
            {
                List<BerariiBrewery2> breweryList = jtoken["brewery"].ToObject<List<BerariiBrewery2>>();
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
            public static explicit operator BerariiRootObject(JToken jtoken)
            {
                return new BerariiRootObject((BerariiLinks)jtoken["_links"], (BerariiEmbedded)jtoken["_embedded"]);
            }
        }
    }
}
