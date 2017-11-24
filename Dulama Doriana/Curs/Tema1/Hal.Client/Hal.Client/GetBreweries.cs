using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    class GetBreweries
    {
        public GetBreweries(string url)
        {
            Program.breweriesList = new List<Brewery>();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");

            //var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries").Result;
            var response = client.GetAsync(url).Result;

            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject(data);

            var brewerys = JsonConvert.DeserializeObject<Data>(data);
            int nrBrew = brewerys.embedded.brewery.Count;

            for (int i = 0; i < nrBrew; i++)
                Program.breweriesList.Add(brewerys.embedded.brewery[i]);
        }
    }
}
