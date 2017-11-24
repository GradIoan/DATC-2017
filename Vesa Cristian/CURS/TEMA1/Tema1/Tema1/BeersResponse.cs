using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1
{
    public class BeersResponse
    {
        [JsonProperty(PropertyName = "TotalResults")]
        public int TotalResults { get; set; }

        [JsonProperty(PropertyName = "TotalPages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "Page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public BeersResponseLinks Links { get; set; }

        [JsonProperty(PropertyName = "_embedded")]
        public BeersResponseContent Content { get; set; }
    }

    public class BeersResponseLinks
    {
        [JsonProperty(PropertyName = "self")]
        public Reference Self { get; set; }

        [JsonProperty(PropertyName = "page")]
        public List<Reference> Pages { get; set; }

        [JsonProperty(PropertyName = "beer")]
        public List<Reference> Beers { get; set; }
    }

    public class BeersResponseContent
    {
        [JsonProperty(PropertyName = "beer")]
        private List<Beer> Beers { get; set; }
    }
}
