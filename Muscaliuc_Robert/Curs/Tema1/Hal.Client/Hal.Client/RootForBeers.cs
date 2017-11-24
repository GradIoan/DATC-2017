using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    public class RootForBeers
    {
        [JsonProperty("TotalResults")]
        public int TotalResults { get; set; }
        [JsonProperty("TotalPages")]
        public int TotalPages { get; set; }
        [JsonProperty("Page")]
        public int Page { get; set; }
        [JsonProperty("ResourceList")]
        public List<Beers> Beers { get; set; }
        [JsonProperty("_links")]
        public List<object> _links { get; set; }
    }
}
