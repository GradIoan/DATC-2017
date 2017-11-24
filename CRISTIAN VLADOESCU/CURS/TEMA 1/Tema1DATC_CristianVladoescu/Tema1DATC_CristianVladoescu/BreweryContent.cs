using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tema1DATC_CristianVladoescu
{
    public class BreweryContent
    {
        [JsonProperty(PropertyName = "self")]
        public Reference Self { get; set; }

        [JsonProperty(PropertyName = "beers")]
        public Reference Beers { get; set; }
    }
}
