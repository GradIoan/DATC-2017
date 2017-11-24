using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    public class RootObject
    {
        [JsonProperty("ResourceList")]
        public List<Breweries> Breweries { get; set; }
        [JsonProperty("_links")]
        public List<object> _links { get; set; }
    }
}
