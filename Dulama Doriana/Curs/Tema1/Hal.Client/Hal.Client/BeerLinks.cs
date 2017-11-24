using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    class BeerLinks
    {
        [JsonProperty(PropertyName = "self")]
        public Self self { get; set; }

        [JsonProperty(PropertyName = "style")]
        public Style beerStyle { get; set; }

        [JsonProperty(PropertyName = "brewery")]
        public BreweryLinks beerBrewery { get; set; }


    }
}
