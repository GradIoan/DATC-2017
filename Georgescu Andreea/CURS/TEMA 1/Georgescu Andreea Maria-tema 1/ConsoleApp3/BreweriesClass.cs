using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConsoleApp3
{

     class  RootObj
    {
        [JsonProperty("_links")]
        public Links _links { get; set; }
        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }
    }
    class Links
    {
        [JsonProperty("self")]
        public HrefClass Hrefobj { get; set; }
        [JsonProperty("brewery")]
        public List<HrefClass> HrefList { get; set; }
    }
    class HrefClass
    {
        public string href { get; set; }
    }
    class Embedded
    {
        [JsonProperty("brewery")]
        public List<Brewery2> BreweriesList { get; set; }
    }
    class Brewery2
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("_links")]
        public Links2 LinksClass { get; set; }
    }
    class Links2
    {
        [JsonProperty("self")]
        public HrefClass Hrefobj2 { get; set; }
        [JsonProperty("beers")]
        public HrefClass Hrefobj3 { get; set; }
    }
}