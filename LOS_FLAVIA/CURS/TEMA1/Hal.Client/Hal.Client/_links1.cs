using Hal.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATC
{
    class _links
    {
        Beers beer;
        Self self;
        [JsonProperty("beers")]
        internal Beers Beer
        {
            get
            {
                return beer;
            }

            set
            {
                beer = value;
            }
        }
        [JsonProperty("self")]
        internal Self Self
        {
            get
            {
                return self;
            }

            set
            {
                self = value;
            }
        }
    }
}
