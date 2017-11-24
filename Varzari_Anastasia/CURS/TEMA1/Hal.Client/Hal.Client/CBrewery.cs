using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    public class CBrewery
    {
        [JsonProperty("Id")]
        public int breweryId { get; set; }

        [JsonProperty("Name")]
        public string breweryName { get; set; }

        [JsonProperty("_links")]
        public CBrewreyLinks breweryLinks { get; set; }
    }

    public class CBrewreyLinks
    {
        //beers and self
        [JsonProperty("beers")]
        public CBrewerywBeers beers { get; set; }

        [JsonProperty("self")]
        public CBrewerySelf self { get; set; }

    }

    public class CBrewerywBeers
    {
        [JsonProperty("href")]
        public string hrefBrewerywBeers { get; set; }
    }

    public class CBrewerySelf
    {
        [JsonProperty("href")]
        public string hrefBrewerySelf { get; set; }
    }

    public class CHomeBreweryLinks
    {
        [JsonProperty("href")]
        public string hrefBrewery { get; set; }

    }
}
