using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    class Embedded
    {
        [JsonProperty(PropertyName = "brewery")]
        //public Brewery[] brewery { get; set; }
        public List<Brewery> brewery { get; set; }

        [JsonProperty(PropertyName = "beer")]
        public List<Beer> beer { get; set; }
    }
}
