using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    class GetBeers
    {
        public GetBeers(string url)
        {
            Program.breweriesList = new List<Brewery>();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");

            //var response = client.GetAsync("http://datc-rest.azurewebsites.net/beers").Result;
            var response = client.GetAsync(url).Result;

            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject(data);

            var beers = JsonConvert.DeserializeObject<Data>(data);
            int nrBeers = beers.embedded.brewery.Count;

            for (int i = 0; i < nrBeers; i++)
                Program.beersList.Add(beers.embedded.beer[i]);
        }
    }
}
