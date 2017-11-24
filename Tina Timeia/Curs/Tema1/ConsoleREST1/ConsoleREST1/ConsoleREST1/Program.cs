using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleREST1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries").Result;

            var data = response.Content.ReadAsStringAsync().Result;
            // Console.WriteLine(data);
            
            dynamic obj = JsonConvert.DeserializeObject<tot>(data);
         
            Console.WriteLine(obj);
            List<tot.Beer> abc = new List<tot.Beer>();
            abc = obj.embedded.berarii;
            foreach (tot.Beer x in abc)
            {
                Console.WriteLine(x.Id + "  " + x.Name + "  " + x._links);
                Console.WriteLine();
            }
        }
    }
}
