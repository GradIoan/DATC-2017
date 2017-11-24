using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class ApiLogic
    {
        public static HttpResponseMessage CreateRequest(string domain, string method, Beer bere)
        {
            string link1 = "http://datc-rest.azurewebsites.net";
            string link2 = "http://datc-rest.azurewebsites.net/beers";
            var client = new HttpClient();
            var response = new HttpResponseMessage();
            
            if (bere != null)//post
            {
                response = client.PostAsJsonAsync(link2, bere).Result;
            }
            else
            {//get 
                client.DefaultRequestHeaders.Add("accept", "application/hal+json");
                string entryPoint = link1 + domain;
                response = client.GetAsync(entryPoint).Result;
            }

            return response;
        }
    }
}
