using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace ConsoleApp3
{
    class Connect
    {
        public static string ConnectWithAccept()
        {
             var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return data;
            
        }
        public static string  Connect1(string href)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://datc-rest.azurewebsites.net/");
            var response = client.GetAsync(href).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return data;
        }
    }
}
