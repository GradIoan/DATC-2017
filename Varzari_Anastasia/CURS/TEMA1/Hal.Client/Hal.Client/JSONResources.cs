using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    public class JSONResources
    {
        [JsonProperty("_embedded")]
        public CHomeEmbedded homeEmbedded { get; set; }

        [JsonProperty("_links")]
        public CHomeLinks homeLinks { get; set; }

    }

    public partial class CHomeEmbedded
    {
        [JsonProperty("brewery")]
        public CBrewery[] brewery { get; set; }
    }

    public class CHomeLinks
    {
        [JsonProperty("brewery")]
        public List<CHomeBreweryLinks> brewery { get; set; }

        [JsonProperty("self")]
        public CHomeSelfLinks selfLink { get; set; }
    }

    public class CHomeSelfLinks
    {
        [JsonProperty("href")]
        public string hrefHome { get; set; }
    }
}
