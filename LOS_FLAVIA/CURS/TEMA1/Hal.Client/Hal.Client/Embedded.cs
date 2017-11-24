using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class Embedded
    {
        Brewery2[] brewery;

        [JsonProperty("brewery")]
        internal Brewery2[] Brewery
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
