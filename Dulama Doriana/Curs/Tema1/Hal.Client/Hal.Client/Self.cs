using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    class Self
    {
        [JsonProperty(PropertyName = "href")]
        public string href { get; set; }

        public string toString()
        {
            return href;
        }
    }
}
