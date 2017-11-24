using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class Page
    {
        string href;
        string templated;

        [JsonProperty("href")]
        public string Href
        {
            get
            {
                return href;
            }

            set
            {
                href = value;
            }
        }

        [JsonProperty("templated")]
        public string Templated
        {
            get
            {
                return templated;
            }

            set
            {
                templated = value;
            }
        }
    }
}
