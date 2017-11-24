using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tema1.Endpoints
{
    public class Content
    {
        [JsonProperty(PropertyName = "self")]
        public Reference Self { get; set; }

        [JsonProperty(PropertyName = "style")]
        public Reference Style { get; set; }

        [JsonProperty(PropertyName = "brewery")]
        public List<Brewery> Brewery { get; set; }
    }
}
