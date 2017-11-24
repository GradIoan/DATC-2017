using Newtonsoft.Json;

namespace Tema1.Endpoints
{
    public class BreweryContent
    {
        [JsonProperty(PropertyName = "self")]
        public Reference Self { get; set; }

        [JsonProperty(PropertyName = "beers")]
        public Reference Beers { get; set; }
    }
}
