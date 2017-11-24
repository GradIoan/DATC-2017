using Newtonsoft.Json;

namespace Tema1.Endpoints
{
    public class Reference
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "templated")]
        public bool Templated { get; set; }
    }
}
