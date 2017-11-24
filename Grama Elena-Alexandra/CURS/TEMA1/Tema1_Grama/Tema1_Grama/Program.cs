using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Net.Http.Headers;

namespace Tema1_Grama
{
    class Program
    {
        static public string getUrl()
        {
            return "http://datc-rest.azurewebsites.net";
        }

            static void Post(string url, int Id, string Name)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            string url1 = getUrl() + "/beers";
            Beers newBeer = new Beers(Id, Name);
            StringContent content = new StringContent(JsonConvert.SerializeObject(newBeer));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var clientResponse = client.PostAsync(url1, content).Result;
            Console.WriteLine(clientResponse);
        }

         static void Main(string[] args)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            
            var response = client.GetAsync(getUrl() + "/breweries").Result;

            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<RootObject>(data);
            var request = (HttpWebRequest)WebRequest.Create(getUrl() + "/beers");
           
            Brewery2 brew = new Brewery2();

            string selection;
            Console.WriteLine("[1] Breweries");
            Console.WriteLine("[2] POST");
            Console.WriteLine("[3] Exit");
            Console.WriteLine("Optiune: ");
            
            selection = Console.ReadLine();
            switch (selection)
            {
                case "1":
                    obj._embedded.MeniuEmbedded();
                    break;
                case "2":
                    {
                        string ID, name;
                        Console.WriteLine("ID:");
                        brew.Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Name:");
                        brew.Name = Console.ReadLine();
                        Post(getUrl() + "/breweries", brew.Id, brew.Name);
                    }
                   
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
            }

         
        }


    }
}
