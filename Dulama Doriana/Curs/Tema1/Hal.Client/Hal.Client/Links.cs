using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    class Links
    {
        [JsonProperty(PropertyName = "self")]
        public Self self { get; set; }

        [JsonProperty(PropertyName = "brewery")]
        public List<BreweryLinks> breweryLinks { get; set; }

        [JsonProperty(PropertyName = "next")]
        public Next next { get; set; }

        [JsonProperty(PropertyName = "page")]
        public List<Page> page { get; set; }

        [JsonProperty(PropertyName = "beer")]
        public List<BeerList> beerList { get; set; }

        [JsonProperty(PropertyName = "beers")]
        public BeerList beers { get; set; }



        public string toString()
        {
            return self.toString() + " beers: "; //+ beers.toString();
        }
    }
}
