using Newtonsoft.Json;

namespace Tema1.Endpoints
{
    public class Endpoints
    {
        [JsonProperty(PropertyName = "_links")]
        public Content Links { get; set; }
        
        [JsonProperty(PropertyName = "_embedded")]
        public Content Embedded { get; set; }
    }
}
