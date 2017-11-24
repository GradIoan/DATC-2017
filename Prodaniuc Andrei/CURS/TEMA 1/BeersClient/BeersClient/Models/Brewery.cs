using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeersClient.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Brewery
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("_links")]
        public BeerLinks Links { get; set; }
    }
}
