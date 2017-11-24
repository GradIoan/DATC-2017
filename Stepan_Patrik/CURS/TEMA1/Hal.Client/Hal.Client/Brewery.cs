using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    public class BreweryRef
    {
        [JsonProperty("href")]
        public string href { get; set; }
    }

    public class BreweryEmbedded
    {
        [JsonProperty("brewery")]
        public Brewery[] brewery { get; set; }
    }

    public class Brewery
    {
        [JsonProperty("Id")]
        public long id { get; set; }

        [JsonProperty("Name")]
        public string name { get; set; }

        [JsonProperty("_links")]
        public BreweryLinks links { get; set; }

        public static void PrintList(List<Brewery> breweries)
        {
            int index = 0;
            Console.Clear();
            Console.WriteLine("-----------------------> BREWERIES <-----------------------");
            foreach (Brewery el in breweries)
                Console.WriteLine(index++.ToString() + ": " + el.name);
            Console.WriteLine("------------------------------------------------------------");
        }

        public static List<Brewery> GetBrewriesRequest(HttpClient client)
        {
            var response = client.GetAsync("/breweries").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<BreweryResource>(data);

            return obj.embedded.brewery.ToList();
        }
    };

    public class BreweryLinks : Links
    {
        [JsonProperty("beers")]
        public BeerRef beers { get; set; }
    }

    public class BreweryExtLinks : Links
    {
        [JsonProperty("brewery")]
        public BreweryRef[] brewery { get; set; }
    }

    public class BreweryResource
    {
        [JsonProperty("_embedded")]
        public BreweryEmbedded embedded { get; set; }

        [JsonProperty("_links")]
        public BreweryExtLinks links { get; set; }

    }


}
