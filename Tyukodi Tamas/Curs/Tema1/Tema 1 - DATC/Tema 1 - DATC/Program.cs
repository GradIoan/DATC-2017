using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Text;
 using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

using System.Net.Http;

namespace Tema_1___DATC
{
    class Program
    {
        public static HttpClient client = new HttpClient();
        public static string adress = "http://datc-rest.azurewebsites.net/";
        public static List<Beers> listBeers = new List<Beers>();
        public static List<Breweries> listBreweries = new List<Breweries>();
        
        public static void DisplayBreweryApi()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync(adress + "breweries").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject(data);
            Console.WriteLine(obj);
        }
        public static void listBreweriesRefresh()
        {
            listBreweries.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync(adress + "breweries").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject(data);
            data = urlReplace(data);
            string[] split = data.Split(new string[] { "_embedded" }, StringSplitOptions.RemoveEmptyEntries);
            split = split[1].Split(new string[] { "Id:" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length - 1; i++)
            {
                string[] dataBrewery = split[i + 1].Split(new string[] { "Name:" }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(dataBrewery[0]);
                dataBrewery = dataBrewery[1].Split(new string[] { "_links:" }, StringSplitOptions.RemoveEmptyEntries);
                string name = dataBrewery[0];
                dataBrewery = dataBrewery[1].Split(new string[] { "href:" }, StringSplitOptions.RemoveEmptyEntries);
                string linkToBrewerie = dataBrewery[1].Replace("beers:", "");
                string linkToBeers = dataBrewery[2];
                listBreweries.Add(new Breweries(id, name, linkToBeers, linkToBrewerie));
                Console.WriteLine(dataBrewery[1]);
            }
            Console.Clear();

        }
        public static void DisplayBreweries()
        {
            listBreweries.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync(adress + "breweries").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject(data);
            data = urlReplace(data);
            string[] split = data.Split(new string[] { "_embedded" }, StringSplitOptions.RemoveEmptyEntries);
            split = split[1].Split(new string[] { "Id:" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length - 1; i++)
            {
                string[] dataBrewery = split[i + 1].Split(new string[] { "Name:" }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(dataBrewery[0]);
                dataBrewery = dataBrewery[1].Split(new string[] { "_links:" }, StringSplitOptions.RemoveEmptyEntries);
                string name = dataBrewery[0];
                dataBrewery = dataBrewery[1].Split(new string[] { "href:" }, StringSplitOptions.RemoveEmptyEntries);
                string linkToBrewerie = dataBrewery[1].Replace("beers:", "");
                string linkToBeers = dataBrewery[2];
                listBreweries.Add(new Breweries(id, name, linkToBeers, linkToBrewerie));
                Console.WriteLine(dataBrewery[1]);
            }
            Console.Clear();
            Console.WriteLine("");
            for (int i = 0; i < listBreweries.Count; i++)
            {
                Console.WriteLine("ID: " + listBreweries[i].Id);
                Console.WriteLine("Name: " + listBreweries[i].Name);
                Console.WriteLine("");
           }
            
        }
        public static void DisplayBeersApi()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync(adress + "beers").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject(data);
            Console.WriteLine(obj);
        }
        public static string DisplayBreweries2(string value)
         {
            Console.Clear();
             string linkToBeers = "";
             var response = client.GetAsync(adress + value).Result;
             string data = response.Content.ReadAsStringAsync().Result;
             data = urlReplace(data);
             string[] dataBrewery = data.Split(new string[] { "Id:" }, StringSplitOptions.RemoveEmptyEntries);
             dataBrewery = dataBrewery[0].Split(new string[] { "Name:" }, StringSplitOptions.RemoveEmptyEntries);
             int id = int.Parse(dataBrewery[0]);
             dataBrewery = dataBrewery[1].Split(new string[] { "_links:" }, StringSplitOptions.RemoveEmptyEntries);
             string name = dataBrewery[0];
             dataBrewery = dataBrewery[1].Split(new string[] { "href:" }, StringSplitOptions.RemoveEmptyEntries);
             linkToBeers = dataBrewery[2];
             Console.WriteLine("Details about brewery and it's content");
             Console.WriteLine("");
             Console.WriteLine("Brewery ID: "+ id);
             Console.WriteLine("Brewery name: "+name); 
             return linkToBeers;
         }
        public static string urlReplace(string data)
        {
            data = data.Replace("{", "");
            data = data.Replace("}", "");
            data = data.Replace("\"", "");
            data = data.Replace(",", "");
            data = data.Replace("]", "");
            data = data.Replace("[", "");
            return data;
        }
        public static void DisplayBeers(string info)
         {
            listBeers.Clear();
            string linkToBeer = "";
            string linkToStyle = "";
            string linkToBrewery = "";
             var response = client.GetAsync(adress + info).Result;
             string data = response.Content.ReadAsStringAsync().Result;
             data = data.Replace("BreweryId", "Brewery");
             data = data.Replace("StyleId", "Style");
             data = urlReplace(data);
             string[] split = data.Split(new string[] { "_embedded" }, StringSplitOptions.RemoveEmptyEntries);
             split = split[1].Split(new string[] { "Id:" }, StringSplitOptions.RemoveEmptyEntries);
             for (int i = 0; i<split.Length - 1; i++)
             {
                 string[] dataBeer = split[i + 1].Split(new string[] { "Name:" }, StringSplitOptions.RemoveEmptyEntries);
                 int id = int.Parse(dataBeer[0]);
                 string[] dataBeer1 = dataBeer[1].Split(new string[] { "Brewery:" }, StringSplitOptions.RemoveEmptyEntries);
                 string name = dataBeer1[0];
                dataBeer1[1] = dataBeer1[1].Replace("Brewery", "");
                 int idBrewery = int.Parse(dataBeer1[1]);
                 string[] dataBeer2 = dataBeer[2].Split(new string[] { "Style:" }, StringSplitOptions.RemoveEmptyEntries);
                 string nameBrewery = dataBeer2[0];
                dataBeer2[1] = dataBeer2[1].Replace("Style", "");
                 int idStyle = int.Parse(dataBeer2[1]);
                dataBeer = dataBeer[3].Split(new string[] { "_links:" }, StringSplitOptions.RemoveEmptyEntries);
                 string nameStyle = dataBeer[0];
                dataBeer = dataBeer[1].Split(new string[] { "href:" }, StringSplitOptions.RemoveEmptyEntries);
                 linkToBeer = dataBeer[1].Replace("style:", " ");
                linkToStyle = dataBeer[2].Replace("brewery:", "");
                linkToBrewery = dataBeer[3];
                 listBeers.Add(new Beers(id, name, idBrewery, nameBrewery, idStyle, nameStyle, linkToBeer, linkToBrewery, linkToStyle, ""));
             }
             

             for (int i = 0; i< listBeers.Count; i++)
             {
                Console.WriteLine("");
                Console.WriteLine("Id:" + listBeers[i].Id);
                 Console.WriteLine("Name:" + listBeers[i].Name);
                 Console.WriteLine("Style ID:" + listBeers[i].IdStyle);
                 Console.WriteLine("Style Name:" + listBeers[i].NameStyle);
                Console.WriteLine("");
             }
            Console.WriteLine("Enter any key to go back!");
            Console.ReadLine();
         }
        public static void AddBeer()
        {
           
            int id = 0;
            string name = "";     
            Console.Write("Enter beer ID: ");
            int.TryParse(Console.ReadLine(), out id);
            Console.Write("Enter beer name:");
            name = Console.ReadLine();
            HttpContent data = new FormUrlEncodedContent(new[]
             {
                        new KeyValuePair<string,string>("Id:",id.ToString()),
                        new KeyValuePair<string,string>("Name:",name)
                     });

            var exe = client.PostAsync("http://datc-rest.azurewebsites.net/beers", data);
        }
    static void Main(string[] args)
        {
            var opt = 1;
            var option = 0;

            while (opt != 0)
            {
                Console.Clear();
                Console.WriteLine("1) Display all breweries");
                Console.WriteLine("2) Search brewery by ID");
                Console.WriteLine("3) Display Brewery API content");
                Console.WriteLine("4) Display Beers API content");
                Console.WriteLine("5) Add beer");
                Console.WriteLine("6) Exit");
                Console.Write("Choose an option: ");
                Console.WriteLine("");
                int.TryParse(Console.ReadLine(), out option);
                if (option == 1)
                {
                    Console.Clear();
                    DisplayBreweries();
                    Console.ReadLine();
                }
                if (option == 2)
                {                                  
                    listBreweriesRefresh();
                    Console.WriteLine("Enter a brewery ID:");
                    option=  int.Parse(Console.ReadLine());
                    string data=DisplayBreweries2(listBreweries[option -1].LinkToBrewery);
                     DisplayBeers(data);
                }
                if (option == 3)
                {
                    DisplayBreweryApi();
                    Console.Write("Enter any key to go back: ");
                    Console.ReadLine();
                }
                if (option == 4)
                {
                    DisplayBeersApi();
                    Console.Write("Enter any key to go back: ");
                    Console.ReadLine();
                }
                if (option == 5)
                {
                    Console.Clear();
                    AddBeer();
                }
                if (option == 6)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
