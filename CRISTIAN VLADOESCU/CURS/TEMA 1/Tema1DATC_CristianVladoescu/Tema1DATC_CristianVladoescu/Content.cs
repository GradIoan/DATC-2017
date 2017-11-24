using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tema1DATC_CristianVladoescu
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
