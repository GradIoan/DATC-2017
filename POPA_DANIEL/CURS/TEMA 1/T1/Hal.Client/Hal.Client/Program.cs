using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;

namespace Hal.Client
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static HttpResponseMessage response;
        static Newtonsoft.Json.Linq.JObject obj;
        static string Api = "http://datc-rest.azurewebsites.net";
        static string data;
        static string opt, opt2, opt3;
        static List<string> breweries_links = new List<string>();

        static void Main(string[] args)
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            string endpoint = "http://datc-rest.azurewebsites.net/breweries";
            string Api = endpoint.Substring(0, 34);
            var response = client.GetAsync(endpoint).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

            Newtonsoft.Json.Linq.JToken breweries_links_array = obj.First.Last.Last.First;
            create_breweries_list(breweries_links_array);
           
            int opt;
            do
            {
                Console.WriteLine("*******MENIU*******");
                Console.WriteLine("1.Show breweries\n");
                Console.WriteLine("2.Show beers style\n");
                Console.WriteLine("3.Show beers from a specific breweries\n");
                Console.WriteLine("4.Show beers from a specific style\n");
                Console.WriteLine("0.Exit\n");

                opt = Convert.ToInt16(Console.ReadLine());
                switch (opt)
                {
                    case 1:
                        Console.WriteLine("\nList of breweries: \n");
                        breweries();
                        break;
                    case 2:
                        Console.WriteLine("\nList of beers style: \n");
                        beers_style();
                        break;
                    case 3:
                        Console.WriteLine("\nInsert the ID of brewery for which you want to see beers list:");
                        opt2 = Console.ReadLine();
                        beers_brewery(opt2);
                        break;
                    case 4:
                        Console.WriteLine("\nInsert the ID of beer style for which you want to see beers list:");
                        opt3 = Console.ReadLine();
                        beers_style_id(opt3);
                        break;
                    case 0:
                        Console.WriteLine("Have a nice day! :)\n");
                        break;
                    default:
                        Console.WriteLine("Introduced option does not exist! Introduce a valid option[1,2,3 or 4]\n");
                        break;
                }

            } while (opt != 0);
            Console.ReadKey();
        }

        static public void create_breweries_list(Newtonsoft.Json.Linq.JToken breweries_links_array)
        {
            foreach (var i in breweries_links_array)
            {
                breweries_links.Add(Convert.ToString(i.First.First));
            }
        }
        static public void breweries()
        {
            foreach (var i in breweries_links)
            {
                response = client.GetAsync(Api + i.ToString()).Result;
                data = response.Content.ReadAsStringAsync().Result;
                obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

                if (!obj.First.ToString().Equals("\"Message\": \"An error has occurred.\""))
                {
                    Console.WriteLine("Breweries ID: " + obj.First.First);
                    Console.WriteLine("Breweries name: " + obj.First.Next.First +"\n");
                }
            }
        Console.WriteLine("\nPress any key to return in main menu:\n");
        Console.ReadLine();
        }

        static public void beers_brewery(string opt2)
        {
            Console.WriteLine("List of beers: \n");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");

            response = client.GetAsync(Api + "/breweries/" + opt2 + "/beers").Result;
            data = response.Content.ReadAsStringAsync().Result;
            obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

            foreach (var i in obj.Last.First.First.First)
            {
                try
                {
                    Console.WriteLine("Beer ID : " + i.First.First);
                    Console.WriteLine("Beer name : " + i.First.Next.First + "\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Brewery ID is invalid. \n");
                }
            }
            Console.WriteLine("\nPress any key to return in main menu:\n");
            Console.ReadLine();
        }

        static public void beers_style()
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");

            response = client.GetAsync(Api + "/styles").Result;
            data = response.Content.ReadAsStringAsync().Result;
            obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);
            foreach (var i in obj.Last.First.First.First)
            {
                Console.WriteLine("ID beer style: " + i.First.First);
                Console.WriteLine("Name beer style: " + i.First.Next.First + "\n");
            }
            Console.WriteLine("\nPress any key to return in main menu:\n");
            Console.ReadLine();
        }
       
        static public void beers_style_id(string opt)
        {
            Console.WriteLine("List of beers style after an ID: \n");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");

            response = client.GetAsync(Api + "/styles/" + opt + "/beers").Result;
            data = response.Content.ReadAsStringAsync().Result;
            obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

            foreach (var i in obj.Last.First.First.First)
            {
                try
                {
                    Console.WriteLine("Beer ID :" + i.First.First);
                    Console.WriteLine("Beer name :" + i.First.Next.First);
                    Console.WriteLine("Breweries names :" + i.First.Next.Next.Next.First+ "\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Style ID is invalid!");
                }
            }
        }
    }
}
