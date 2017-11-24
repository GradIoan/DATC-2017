using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace tema_1
{
    class Program
    {

        private const string BaseUrl = "http://datc-rest.azurewebsites.net/breweries";
        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }
        static async void MainAsync(string[] args)
        {

            int option;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);

            // Add an Accept header for JSON format. 
            apiMethods api = new apiMethods();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            string stringResponse = await response.Content.ReadAsStringAsync();
            JObject ans = JObject.Parse(stringResponse);
            JObject jObj = (JObject)JsonConvert.DeserializeObject(stringResponse);
            int count = jObj.Count;
            //int count = ans.Count;
            
            do
            {
                Console.WriteLine("1.List beers");
                Console.WriteLine("2.List beer");
                Console.WriteLine("3.Add beer");
                Console.WriteLine("4.Edit beer");
                Console.WriteLine("5.Delete beer");
                Console.WriteLine("0.Exit");
                Console.WriteLine("Please enter your option:");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 0:
                        //EXIT
                        break;
                    case 1:
                        //GET
                        Console.Write("Choose the brewery's id: ");
                        var bId = Console.ReadLine();
                        response = await client.GetAsync(new Uri(BaseUrl+"/"+bId));
                        stringResponse = await response.Content.ReadAsStringAsync();
                        var obj = JsonConvert.DeserializeObject(stringResponse);
                        var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                        Console.WriteLine(json + "\nStatus code: " + response.StatusCode);
                        if (Convert.ToInt32(bId) > count) { Console.WriteLine("Nu exista id-ul berariei!"); break; }
                        break;
                    case 2:
                        //GET
                        api.getBeerAsync(BaseUrl, client, response, stringResponse, count);
                        break;
                    case 3:
                        //POST
                        api.addBeerAsync(client, BaseUrl);
                        break;
                    case 4:
                        //PUT
                        api.editBeerAsync(BaseUrl, client, count);
                        break;
                    case 5:
                        //DELETE
                        api.deleteBeerAsync(client, BaseUrl, count);
                        break;

                    default:
                        Console.WriteLine("Wrong Command!");
                        break;
                }//end switch
            } while (option != 0);

        }
    }
}
