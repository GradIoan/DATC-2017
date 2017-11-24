using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    class Data
    {
        [JsonProperty(PropertyName = "_links")]
        public Links links { get; set; }

        [JsonProperty(PropertyName = "_embedded")]
        public Embedded embedded { get; set; }

        [JsonProperty(PropertyName = "TotalResults")]
        public string TotalResults { get; set; }

        [JsonProperty(PropertyName = "TotalPages")]
        public string TotalPages { get; set; }

        [JsonProperty(PropertyName = "Page")]
        public string Page { get; set; }

    }
}
