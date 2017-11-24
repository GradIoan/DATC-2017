using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            string home = "http://datc-rest.azurewebsites.net/breweries";
            Dictionary<int, string> breweries = new Dictionary<int, string>();
            Dictionary<int, string> beers = new Dictionary<int, string>();

            string opt;

            breweries = get_breweries(breweries, client, home, "_embedded.brewery[*]");

            do
            {
                breweries_menu(breweries);
                opt = Console.ReadLine();

                if (opt != "e")
                {
                    if (Convert.ToInt32(opt) != 0)
                    {
                        beers = get_beers(beers, client, home + "/" + opt + "/beers", "_embedded.beer[*]");
                        display_beers(beers);
                    }
                    else
                        add_beer();
                }
                else
                    break;
            }while (opt != "e");

                Console.ReadLine();
        }

        static void breweries_menu(Dictionary<int, string> breweries)
        {
            Console.WriteLine("\n----------Welcome----------\n");
            Console.WriteLine(" (Press e for Exit) ");
            Console.WriteLine(" (Press 0 to add Beer) ");
            foreach (KeyValuePair<int, string> brewery in breweries)
            {
                Console.WriteLine("{0}) {1}", brewery.Key, brewery.Value);
            }
            Console.WriteLine("\nSelect a brewery: ");
        }

        static void display_beers(Dictionary<int, string> beers)
        {
            Console.WriteLine("\n--------Beers for selected brewery--------\n");
            foreach (KeyValuePair<int, string> beer in beers)
            {
                Console.WriteLine("-- {0}", beer.Value);
            }
        }

        static System.Collections.Generic.Dictionary<int, string> get_beers(
            Dictionary<int, string> values, System.Net.Http.HttpClient client, string link, string tokens_link)
        {
            int id;
            string name;
            JObject jObj = new JObject();

            var response = client.GetAsync(link).Result;
            string data = response.Content.ReadAsStringAsync().Result;

            var root = JToken.Parse(data);
            var myThings = root.SelectTokens(tokens_link).ToList();

            for (int i = 0; i < myThings.Count; i++)
            {
                jObj = JObject.FromObject(myThings[i]);
                id = Convert.ToInt32(myThings[i]["Id"]);
                name = myThings[i]["Name"].ToString();
                values.Add(id, name);
            }
            return values;
        }

        static System.Collections.Generic.Dictionary<int, string> get_breweries(
            Dictionary<int, string> values, System.Net.Http.HttpClient client, string link, string tokens_link)
        {
            int id;
            string name;
            JObject jObj = new JObject();

            var response = client.GetAsync(link).Result;
            string data = response.Content.ReadAsStringAsync().Result;

            //var obj = JsonConvert.DeserializeObject(data);
            //Console.WriteLine(obj.ToString());

            var root = JToken.Parse(data);
            var myThings = root.SelectTokens(tokens_link).ToList();

            for (int i = 0; i < myThings.Count - 3; i++)
            {
                jObj = JObject.FromObject(myThings[i]);
                id = Convert.ToInt32(myThings[i]["Id"]);

                response = client.GetAsync(link + "/" + id).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                var brewery = (JObject)JsonConvert.DeserializeObject(json);
                name = brewery["Name"].Value<string>();

                values.Add(id, name);
            }
            return values;
        }

        static void add_beer()
        {
            Dictionary<string, string> beer = new Dictionary<string, string>();
            string home = "http://datc-rest.azurewebsites.net/beers";

            Console.WriteLine("Enter beer Id: ");
            beer.Add("Id", Console.ReadLine());
            Console.WriteLine("Enter beer Name: ");
            beer.Add("Name", Console.ReadLine());
            string json = JsonConvert.SerializeObject(beer, Formatting.Indented);
            StringContent content = new StringContent(json);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var response = client.PostAsync(home, content).Result;
            Console.WriteLine(response);
        }

    }

}
