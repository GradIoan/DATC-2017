using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATC
{
    class ResourceList
    {
        string id;
        string name;
        _links[] linkArray;

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


        [JsonProperty("_links")]
        internal _links[] LinkArray
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
