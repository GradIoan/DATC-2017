using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tema1DATC_CristianVladoescu
{
    public class Endpoints
    {
        [JsonProperty(PropertyName = "_links")]
        public Content Links { get; set; }
        
        [JsonProperty(PropertyName = "_embedded")]
        public Content Embedded { get; set; }
    }
}
