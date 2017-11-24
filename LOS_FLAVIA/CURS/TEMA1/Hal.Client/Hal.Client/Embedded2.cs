using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class Embedded2
    {
        Beer2[] beer;

        [JsonProperty("beer")]
        internal Beer2[] Beer
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
    }
}
