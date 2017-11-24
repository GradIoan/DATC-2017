using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    public class Endpoints
    {
        [JsonProperty(PropertyName = "_links")]
        public Content Links { get; set; }
        
        [JsonProperty(PropertyName = "_embedded")]
        public Content Embedded { get; set; }
    }
}
