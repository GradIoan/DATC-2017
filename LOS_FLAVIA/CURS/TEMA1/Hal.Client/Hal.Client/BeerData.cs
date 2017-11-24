using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class BeerData
    {
        string totalResults;
        string totalPages;
        string page;
        _links3 link;
        Embedded2 embedded;

        [JsonProperty("TotalResults")]
        public string TotalResults
        {
            get
            {
                return totalResults;
            }

            set
            {
                totalResults = value;
            }
        }
        [JsonProperty("TotalPages")]
        public string TotalPages
        {
            get
            {
                return totalPages;
            }

            set
            {
                totalPages = value;
            }
        }
        [JsonProperty("Page")]
        public string Page
        {
            get
            {
                return page;
            }

            set
            {
                page = value;
            }
        }
        [JsonProperty("_links")]
        internal _links3 Link
        {
            get
            {
                return link;
            }

            set
            {
                link = value;
            }
        }
        [JsonProperty("_embedded")]
        internal Embedded2 Embedded
        {
            get
            {
                return embedded;
            }

            set
            {
                embedded = value;
            }
        }
    }
}
