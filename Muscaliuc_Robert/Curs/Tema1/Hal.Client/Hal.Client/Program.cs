using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
	class Program
	{
       
        static void Main(string[] args)
		{
            int opt = 0;         
            var client = new HttpClient();
			var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries").Result;

			var data = response.Content.ReadAsStringAsync().Result;
            //var obj = JsonConvert.DeserializeObject(data);
            RootObject rootObj = JsonConvert.DeserializeObject<RootObject>(data);
            /*var _obj = new RootForBeers();
            _obj.Beers[0].Name = "Desperados";
            _obj.Beers[0].Id = 22;*/

           var values = new Dictionary<string, string>{
                {"Id" , "22"},
                {"Name" , "Desperados"},
                {"_links", " "}

            };


            //string json = JsonConvert.SerializeObject(_obj);
            HttpContent content = new FormUrlEncodedContent(values);
            var responseTwo = client.PostAsync("http://datc-rest.azurewebsites.net/beers", content);
            //Console.WriteLine(responseTwo);
            do
            {
                Console.WriteLine("1. Show all breweries");
                Console.WriteLine("2. Exit");
                Console.WriteLine("Choose your option");
                opt = int.Parse(Console.ReadLine());

                switch (opt)
                {
                    case 1:
                        Console.Clear();
                        foreach (Breweries br in rootObj.Breweries)
                        {
                            Console.WriteLine(br.Id + " " + br.Name);
                            Console.WriteLine("  " + br._links);
                            Console.WriteLine();
                        };
                        break;
                    case 2:
                        return;
                        
                }
               


            }
            while (opt != 2);



            //Console.WriteLine(br.Id);
			//Console.WriteLine(rootObj.Breweries);

			//Console.ReadLine();
		}
	}
}
