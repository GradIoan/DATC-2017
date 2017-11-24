using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class _links2
    {
        Self self;
        Brewery1[] brewery;

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

        [JsonProperty("brewery")]
        internal Brewery1[] Brewery
        {
            get
            {
                return brewery;
            }

            set
            {
                brewery = value;
            }
        }
    }
}
