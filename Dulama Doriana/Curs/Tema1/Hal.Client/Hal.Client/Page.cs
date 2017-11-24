using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    class Page
    {
        [JsonProperty(PropertyName = "href")]
        public string href { get; set; }

        [JsonProperty(PropertyName = "templated")]
        public string template { get; set; }
    }
}
