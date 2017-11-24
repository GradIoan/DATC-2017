using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Tema1DATC_CristianVladoescu
{
    class Program
    {
        private const string URL = "http://datc-rest.azurewebsites.net";

        static int GetMenu()
        {
            int caseSwitch;
            Console.WriteLine("------------------------Menu------------------------");
            Console.WriteLine("1. Print breweries.");
            Console.WriteLine("2. Print some beer.");
            Console.WriteLine("3. Add brewery.");
            Console.WriteLine("4. Add beer in an existent brewery.");
            Console.WriteLine("5. Another way to add beer.");
            Console.WriteLine("0. Exit!");
            Console.WriteLine("Choose an option:");
            caseSwitch = int.Parse(Console.ReadLine());
            return caseSwitch;

        }
        static void Main(string[] args)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response =client.GetAsync(new Uri(URL + "/breweries")).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var endpoints = JsonConvert.DeserializeObject<Endpoints>(data);
            string newURL;
            int selectedOption;
            do
            {
                Console.Clear();
                selectedOption = GetMenu();
                switch (selectedOption)
                {
                    case 1:
                        var obj = JsonConvert.DeserializeObject(data);
                        var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                        Console.WriteLine(json + "\nStatus code: " + response.StatusCode);   
                        Console.ReadLine();
                        break;
                    case 2:
                        var countBreweries = endpoints.Links.Brewery.Count;
                        Console.WriteLine("Choose brewery's ID:");
                        var breweryID = int.Parse(Console.ReadLine());

                        if (breweryID > countBreweries)
                        {
                            Console.WriteLine("This brewery does not exist!");
                            break;
                        }

                        Console.WriteLine("Choose beer's ID:");
                        var beerId = int.Parse(Console.ReadLine());
                        
                        newURL = URL + endpoints.Embedded.Brewery.First(e => e.Id == breweryID).Links.Beers.Href;
                        response = client.GetAsync(new Uri(newURL)).Result;
                        data = response.Content.ReadAsStringAsync().Result;
                        var beers = JsonConvert.DeserializeObject<BeerResponse>(data);
                        if (beerId > beers.Links.Beers.Count)
                        {
                            Console.WriteLine("The beer does not exist!");
                            break;
                        }

                        newURL = URL + beers.Links.Beers[beerId - 1].Href;
                        response = client.GetAsync(new Uri(newURL)).Result;
                        data = response.Content.ReadAsStringAsync().Result;
                        var beer = JsonConvert.DeserializeObject(data);
                        var jsonBeer = JsonConvert.SerializeObject(beer, Formatting.Indented);
                        Console.WriteLine(jsonBeer + "\nStatus code: " + response.StatusCode);
                        Console.ReadLine();
                        break;
                    case 3:
                        Brewery brewery = new Brewery();
                        Console.WriteLine("Give brewery's ID:");
                        brewery.Id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Give brewery's name:");
                        brewery.Name = Console.ReadLine();

                        var jsonBrewery = JsonConvert.SerializeObject(brewery, Formatting.Indented);
                        var contentBrewery = new StringContent(jsonBrewery, Encoding.UTF8, "application/json");

                        newURL = URL + endpoints.Embedded.Brewery;

                        var postResponse = client.PostAsync(newURL, contentBrewery).Result;
                        var postData = postResponse.Content.ReadAsStringAsync().Result;

                        if(postResponse.StatusCode==System.Net.HttpStatusCode.Created)
                        {
                            Console.WriteLine("The brewery was added:\n" + postData);
                        }
                        Console.ReadLine();
                        break;
                    case 4:
                        Beer breweryBeer = new Beer();

                        Console.WriteLine("Give beer's ID:");
                        breweryBeer.Id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Give beer's name:");
                        breweryBeer.Name = Console.ReadLine();
                        Console.WriteLine("Give brewery's ID:");
                        breweryBeer.BreweryId =int.Parse(Console.ReadLine());
                        Console.WriteLine("Give brewery's name");
                        breweryBeer.BreweryName = Console.ReadLine();
                        Console.WriteLine("Give style's name");
                        breweryBeer.StyleId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Give style's name:");
                        breweryBeer.StyleName= Console.ReadLine();

                        var jsonBreweryBeer = JsonConvert.SerializeObject(breweryBeer, Formatting.Indented);
                        var contentBreweryBeer = new StringContent(jsonBreweryBeer, Encoding.UTF8, "application/json");

                        newURL = URL + endpoints.Embedded.Brewery;

                        var postResponseBreweryBeer = client.PostAsync(newURL, contentBreweryBeer).Result;
                        var postDataBreweryBeer = postResponseBreweryBeer.Content.ReadAsStringAsync().Result;

                        if (postResponseBreweryBeer.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            Console.WriteLine("The brewery was added:\n" + postDataBreweryBeer);
                        }
                        Console.ReadLine();
                        break;
                    case 5:
                        client = new HttpClient();
                        Console.WriteLine("Name of the beer you want to add:");
                        string beerName = Console.ReadLine();
                        string beerAdded="{\"Name\":\"" +beerName+ "\"}";
                        var jsonBreweryBeerAdded = JsonConvert.SerializeObject(beerAdded, Formatting.Indented);
                        var contentBreweryBeerAdded = new StringContent(jsonBreweryBeerAdded, Encoding.UTF8, "application/json");
                        var postResponseAddBeer = client.PostAsync("http://datc-rest.azurewebsites.net/beers", contentBreweryBeerAdded);
                        break;
                    default:
                        Console.WriteLine("The program is closing...");
                        break;
                }
            } while (selectedOption != 0);

        }
    }
}
