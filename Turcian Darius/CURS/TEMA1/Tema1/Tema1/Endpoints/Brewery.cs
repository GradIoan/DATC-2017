using Newtonsoft.Json;

namespace Tema1.Endpoints
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
