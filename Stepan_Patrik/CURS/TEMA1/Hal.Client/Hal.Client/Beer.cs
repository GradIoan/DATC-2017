using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    public class BeerRef
    {
        [JsonProperty("href")]
        public string href { get; set; }
    };

    public class Beer
    {
        [JsonProperty("_links")]
        public BeerLinks links { get; set; }

        [JsonProperty("BreweryId")]
        public long breweryId { get; set; }

        [JsonProperty("BreweryName")]
        public string breweryName { get; set; }

        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Name")]
        public string name { get; set; }

        [JsonProperty("StyleId")]
        public long styleId { get; set; }

        [JsonProperty("StyleName")]
        public string styleName {get; set;}

        public static void PrintList(List<Beer> beers)
        {
            int index = 0;
            Console.Clear();
            Console.WriteLine("-----------------------> BEER LIST <-----------------------");
            if(beers != null)
                foreach (Beer beer in beers)
                    Console.WriteLine(index++.ToString() + " " + beer.name);
            Console.WriteLine("------------------------------------------------------------");
        }

        public static void PrintDetails(Beer beer)
        {
            Console.Clear();
            Console.WriteLine("-----------------------> DETAILS <-----------------------");
            Console.WriteLine("Id: " + beer.Id);
            Console.WriteLine("Name: " + beer.name);
            Console.WriteLine("Brewery: " + beer.breweryName);
            Console.WriteLine("Style: " + beer.styleName);
            Console.WriteLine("----------------------------------------------------------");
        }

        public static bool AddBeer(HttpClient client, string beer)
        {
            string json = JsonConvert.SerializeObject(new { Name = beer });
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/hal+json");
            var httpResponse = client.PostAsync("/beers", content);

            return true;
        }

        public static List<Beer> GetBeersRequest(HttpClient client, string beers)
        {
            var response = client.GetAsync(beers).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<BeerResource>(data);

            if (obj != null && obj.embedded != null)
                return obj.embedded.beer;
            else
                return null;
        }

        public static List<Beer> GetBeersRequest(HttpClient client, string beers, long page, string searchTerm,
            out long currentPage, out long totalPages, out long totalResults)
        {
            // Default values in case of error
            currentPage = 1;
            totalPages = 0;
            totalResults = 0;


            var response = /*searchTerm != ""? client.GetAsync(beers + "/?searchTerm=" + searchTerm).Result:*/
                                               client.GetAsync(beers + "/?page=" + page.ToString()).Result ;
            var data = response.Content.ReadAsStringAsync().Result;
            if (data == null)
                return null;
            var obj = JsonConvert.DeserializeObject<BeerResource>(data);
            if (obj == null)
                return null;

            currentPage = obj.page;
            totalPages = obj.totalPages;
            totalResults = obj.totalResults;

            if (obj != null && obj.embedded != null)
                return obj.embedded.beer;
            else
                return null;
        }
    }

    public class BeerEmbedded
    {
        [JsonProperty("beer")]
        public List<Beer> beer { get; set; }
    }

    public class BeerLinks : Links
    {
        [JsonProperty("beer")]
        public List<BeerRef> beer {get; set;}

        [JsonProperty("page")]
        public List<PageRef> page { get; set;}
    }

    public class BeerResource
    {
        [JsonProperty("_embedded")]
        public BeerEmbedded embedded { get; set; }

        [JsonProperty("_links")]
        public BeerLinks links { get; set; }

        [JsonProperty("Page")]
        public long page { get; set; }

        [JsonProperty("TotalPages")]
        public long totalPages { get; set; }

        [JsonProperty("TotalResults")]
        public long totalResults { get; set; }
    }
}
