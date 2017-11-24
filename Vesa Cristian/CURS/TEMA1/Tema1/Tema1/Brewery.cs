using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    public class Brewery
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }


        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }


        [JsonProperty(PropertyName = "_links")]
        public BreweryContent Links { get; set; }
    }
}
