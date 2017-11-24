using BeersClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeersClient.Services
{
    public class HALClientService
    {
        public static string _homeBaseUrl = "http://datc-rest.azurewebsites.net/";
        HttpClient _client;
        public HALClientService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_homeBaseUrl);
            _client.DefaultRequestHeaders.Add("Accept", "application/hal+json");

        }
        public async Task<List<Brewery>> GetBreweries()
        {
            var response = await _client.GetAsync("breweries/");
            var jsonString=response.Content.ReadAsStringAsync().Result;
            var resource = JsonConvert.DeserializeObject<BreweryResource>(jsonString);
            return resource.Embedded.Brewery.ToList();
        }

        public async Task<Brewery> GetBrewery(string brewery)
        {
            var response = await _client.GetAsync(brewery);
            var jsonString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Brewery>(jsonString);
        }

        internal async Task<BeerResource> GetBeers(string beers)
        {
            var response = await _client.GetAsync(beers);
            var jsonString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<BeerResource>(jsonString);
        }
        public async Task<Beer> GetBeer(string beer)
        {
            var response = await _client.GetAsync(beer);
            var jsonString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Beer>(jsonString);
        }
        public async Task<bool> AddBeer(string beerName)
        {
            var beer = JsonConvert.SerializeObject(new { Name = beerName});
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> resource = new KeyValuePair<string, string>("Name",beerName);
            list.Add(resource);
            var content = new FormUrlEncodedContent(list);
            var result = await _client.PostAsync("/beers", content);

            if (result.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
