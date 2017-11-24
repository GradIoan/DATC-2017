using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    class Brewery
    {
        [JsonProperty(PropertyName = "_links")]
        public Links links { get; set; }

        [JsonProperty(PropertyName = "Id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        public string toString()
        {
            return ID + " - " + Name;
        }

    }
}
