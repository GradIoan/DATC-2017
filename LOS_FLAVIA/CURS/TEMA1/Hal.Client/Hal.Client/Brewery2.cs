using DATC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class Brewery2
    {
        string id;
        string name;
        _links linkArray;

        [JsonProperty("_links")]
        internal _links LinkArray
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
        [JsonProperty("Id")]
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        [JsonProperty("Name")]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
}
