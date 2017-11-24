using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class Self
    {
        string href;
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
    }
}
