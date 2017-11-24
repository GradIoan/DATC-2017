using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace tema_1
{
    class apiMethods
    {

        public async void getBeerAsync(string BaseUrl, HttpClient client, HttpResponseMessage response, string stringResponse, int count)
        {
            Console.Write("Choose the brewery's id: ");
            var breweryId = int.Parse(Console.ReadLine());
            if (breweryId > count) { Console.WriteLine("Beer id not found!"); return; }
            Console.Write("Choose the beer's id: ");
            var beerId = int.Parse(Console.ReadLine());
            var url = BaseUrl + "/" + beerId;
            response = await client.GetAsync(new Uri(url));
            stringResponse = await response.Content.ReadAsStringAsync();
            response = await client.GetAsync(new Uri(url));
            stringResponse = await response.Content.ReadAsStringAsync();
            var beer = JsonConvert.DeserializeObject(stringResponse);
            var jsonBeer = JsonConvert.SerializeObject(beer, Formatting.Indented);
            Console.WriteLine(jsonBeer + "\nStatus code: " + response.StatusCode);
        }

        public async void addBeerAsync(HttpClient client, string BaseUrl)
        {
            dynamic br = new JObject();
            Console.Write("Introduceti Id-ul berii: ");
            br.Id = int.Parse(Console.ReadLine());
            Console.Write("Introduceti Id-ul berariei: ");
            br.BreweryId = int.Parse(Console.ReadLine());
            Console.Write("Introduceti numele berii: ");
            br.Name = Console.ReadLine();
            Console.Write("Introduceti numele berariei: ");
            br.BreweryName = Console.ReadLine();
            Console.Write("Introduceti Id-ul stilului: ");
            br.StyleId = int.Parse(Console.ReadLine());
            Console.Write("Introduceti numele stilului: ");
            br.StyleName = Console.ReadLine();
            var jsonBeerFormat = JsonConvert.SerializeObject(br, Formatting.Indented);
            var httpContent = new StringContent(jsonBeerFormat, Encoding.UTF8, "application/json");
            var postResponse = await client.PostAsync(BaseUrl, httpContent);
            var responseString = await postResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Status code: " + postResponse.StatusCode);
            if (postResponse.StatusCode == HttpStatusCode.Created)
            {
                Console.WriteLine("Berea a fost adaugata: \n" + responseString);
            }
        }

        public async Task editBeerAsync(string BaseUrl, HttpClient client, int count)
        {
            Console.Write("Choose the brewery's id: ");
            var breweryId = int.Parse(Console.ReadLine());
            if (breweryId > count) { Console.WriteLine("Beer id not found!"); return; }
            Console.Write("Choose the beer's id: ");
            var beerId = int.Parse(Console.ReadLine());
            var url = BaseUrl + "/" + beerId;
            Console.Write("Choose another name for the beer:");
            var beerName = Console.ReadLine();
            dynamic NameB = new JObject();
            NameB.Name = beerName;
            var jsonBeerFormat = JsonConvert.SerializeObject(NameB, Formatting.Indented);
            var httpContent = new StringContent(jsonBeerFormat, Encoding.UTF8, "application/json");

            var putResponse = await client.PutAsync(url, httpContent);
            var responseString = await putResponse.Content.ReadAsStringAsync();

            Console.WriteLine("Status code: " + putResponse.StatusCode);
            if (putResponse.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Beer edited with success : \n" + responseString);
            }
        }

        public async Task deleteBeerAsync(HttpClient client, string BaseUrl, int count)
        {
            Console.Write("Choose the brewery's id: ");
            var breweryId = int.Parse(Console.ReadLine());
            if (breweryId > count) { Console.WriteLine("Beer id not found!"); return; }

            Console.Write("Choose the beer's id: ");
            var beerId = int.Parse(Console.ReadLine());
            var url = BaseUrl + "/" + beerId;
            var response = await client.DeleteAsync(new Uri(url));
            var stringResponse = await response.Content.ReadAsStringAsync();
            var objDel = JsonConvert.DeserializeObject(stringResponse);
            var jsonObj = JsonConvert.SerializeObject(objDel, Formatting.Indented);
            Console.WriteLine(jsonObj + "\nStatus code: " + response.StatusCode);
        }
    }
}
