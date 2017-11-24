using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeersClient.Models
{
    public class BreweryResource
    {

        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }

        [JsonProperty("_links")]
        public BreweryLinks Links { get; set; }

        public static BreweryResource FromJson(string json)
        {
            return JsonConvert.DeserializeObject<BreweryResource>(json, Converter.Settings);
        }
    }

    public class BeerResource : BreweryResource
    {
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
    }
    public partial class Embedded
    {
        [JsonProperty("brewery")]
        public Brewery[] Brewery { get; set; }
        [JsonProperty("beer")]
        public Beer[] Beer { get; set; }
    }

    public class BeerLinks
    {
        [JsonProperty("beers")]
        public Beers Beers { get; set; }

        [JsonProperty("self")]
        public Beers Self { get; set; }
    }

    public partial class BreweryLinks
    {
        [JsonProperty("brewery")]
        public Beers[] Brewery { get; set; }

        [JsonProperty("self")]
        public Beers Self { get; set; }
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
