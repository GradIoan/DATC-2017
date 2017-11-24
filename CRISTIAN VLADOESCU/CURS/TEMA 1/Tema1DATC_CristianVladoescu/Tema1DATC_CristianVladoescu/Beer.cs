using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tema1DATC_CristianVladoescu
{
    public class Beer
    {
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "BreweryId")]
        public int BreweryId { get; set; }

        [JsonProperty(PropertyName = "BreweryName")]
        public string BreweryName { get; set; }

        [JsonProperty(PropertyName = "StyleId")]
        public int StyleId { get; set; }

        [JsonProperty(PropertyName = "StyleName")]
        public string StyleName { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public BeerLinks Links { get; set; }
    }

    public class BeerLinks
    {
        [JsonProperty(PropertyName = "self")]
        public Reference Self { get; set; }

        [JsonProperty(PropertyName = "style")]
        public Reference Style { get; set; }

        [JsonProperty(PropertyName = "brewery")]
        public Reference Brewery { get; set; }

        [JsonProperty(PropertyName = "review")]
        public List<Reference> Review { get; set; }
    }
}
