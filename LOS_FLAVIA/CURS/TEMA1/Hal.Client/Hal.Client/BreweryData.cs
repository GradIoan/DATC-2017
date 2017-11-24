using Hal.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATC
{
    class Data
    {
       
        _links2 linkArray;
        Embedded embeddedArray;

        [JsonProperty("_links")]
        internal _links2 LinkArray
        {
            get
            {
                return linkArray;
            }

            set
            {
                linkArray = value;
            }
        }
      
        [JsonProperty("_embedded")]
        internal Embedded EmbeddedArray
        {
            get
            {
                return embeddedArray;
            }

            set
            {
                embeddedArray = value;
            }
        }
    }
}
