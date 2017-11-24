using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    public class Links
    {
        /*[JsonProperty("beers")]
        public BeerFef beers { get; set; }*/

        [JsonProperty("self")]
        public BreweryRef self { get; set; }
    };


}
