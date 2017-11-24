using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    public class Reference
    {

        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }


        [JsonProperty(PropertyName = "templated")]
        public bool Templated { get; set; }
    }
}
