using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Nito.AsyncEx;
using EndpointsClass = Tema1.Endpoints.Endpoints;

namespace Tema1
{
    class Program
    {
        
        private const string BaseUrl = "http://datc-rest.azurewebsites.net"; 

        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }

        static async void MainAsync(string[] args)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");

            HttpResponseMessage response =  await client.GetAsync(new Uri(BaseUrl + "/breweries"));
            string stringResponse = await response.Content.ReadAsStringAsync();
            var endpoints = JsonConvert.DeserializeObject<EndpointsClass>(stringResponse);

            var count = endpoints.Links.Brewery.Count;
            var postBeerEndpoint = string.Empty;
            if (endpoints.Embedded != null && endpoints.Embedded.Brewery.Count > 0)
                postBeerEndpoint = "/" + endpoints.Embedded.Brewery[0].Links.Beers.Href.Split('/')[3];

            int opt;
            do
            {
                Console.WriteLine("Alegeti optiunea: ");
                Console.WriteLine("1 - Afisare beri");
                Console.WriteLine("2 - Afiseaza o bere");
                Console.WriteLine("3 - Adauga o bere");
                Console.WriteLine("4 - Editeaza o bere");
                Console.WriteLine("5 - Sterge o bere");
                Console.WriteLine("0 - Exit");

                opt = int.Parse(Console.ReadLine());
                string url;

                switch (opt)
                {
                    //afisare beri
                    case 1:
                        Console.Write("Choose the brewery's id: ");// or brewery's name
                        var breweryId = int.Parse(Console.ReadLine());
                        if(breweryId > count) {Console.WriteLine("Nu exista id-ul berariei!"); break;}

                        if (endpoints.Embedded != null)
                        {
                            url = BaseUrl + endpoints.Embedded.Brewery.First(e => e.Id == breweryId).Links.Beers.Href;
                            response = await client.GetAsync(new Uri(url));
                            stringResponse = await response.Content.ReadAsStringAsync();
                            var obj = JsonConvert.DeserializeObject(stringResponse);
                            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                            Console.WriteLine(json + "\nStatus code: " + response.StatusCode);
                        }
                        break;

                    //afisarea unei beri
                    case 2:
                        Console.Write("Choose the brewery's id: ");//or name
                        breweryId = int.Parse(Console.ReadLine());
                        if (breweryId > count) { Console.WriteLine("Nu exista id-ul berariei!"); break; }

                        Console.Write("Choose the beer's id: ");
                        var beerId = int.Parse(Console.ReadLine());

                        url = BaseUrl + endpoints.Embedded.Brewery.First(e => e.Id == breweryId).Links.Beers.Href;
                        response = await client.GetAsync(new Uri(url));
                        stringResponse = await response.Content.ReadAsStringAsync();
                        var beers = JsonConvert.DeserializeObject<BeersResponse>(stringResponse);

                        if (beerId > beers.Links.Beers.Count) { Console.WriteLine("Nu exista id-ul berii!"); break; }

                        url = BaseUrl + beers.Links.Beers[beerId-1].Href;
                        response = await client.GetAsync(new Uri(url));
                        stringResponse = await response.Content.ReadAsStringAsync();
                        var beer = JsonConvert.DeserializeObject(stringResponse);
                        var jsonBeer = JsonConvert.SerializeObject(beer, Formatting.Indented);
                        Console.WriteLine(jsonBeer + "\nStatus code: " + response.StatusCode);
                        break;

                    //adaugarea unei beri
                    case 3:
                        Beer br = new Beer();
                        Console.Write("Dati Id-ul berii: ");
                        br.Id = int.Parse(Console.ReadLine());
                        Console.Write("Dati Id-ul berariei: ");
                        br.BreweryId = int.Parse(Console.ReadLine());
                        Console.Write("Dati numele berii: ");
                        br.Name = Console.ReadLine();
                        Console.Write("Dati numele berariei: ");
                        br.BreweryName = Console.ReadLine();
                        Console.Write("Dati Id-ul stilului: ");
                        br.StyleId = int.Parse(Console.ReadLine());
                        Console.Write("Dati numele stilului: ");
                        br.StyleName = Console.ReadLine();
                        
                        var jsonBeerFormat = JsonConvert.SerializeObject(br, Formatting.Indented);
                        var httpContent = new StringContent(jsonBeerFormat, Encoding.UTF8, "application/json");
                        url = BaseUrl + postBeerEndpoint;
                        var postResponse = await client.PostAsync(url, httpContent);
                        var responseString = await postResponse.Content.ReadAsStringAsync();

                        Console.WriteLine("Status code: " + postResponse.StatusCode);
                        if (postResponse.StatusCode == HttpStatusCode.Created)
                        {
                            Console.WriteLine("Berea a fost adaugata: \n" + responseString);
                        }
                        break;

                    //editarea unei beri
                    case 4:
                        Console.Write("Choose the brewery's id: ");
                        breweryId = int.Parse(Console.ReadLine());
                        if (breweryId > count) { Console.WriteLine("Nu exista id-ul berariei!"); break; }

                        Console.Write("Choose the beer's id: ");
                        beerId = int.Parse(Console.ReadLine());
                        
                        Console.Write("Alege alt nume pentru bere:");
                        var newName = Console.ReadLine();

                        url = BaseUrl + endpoints.Embedded.Brewery.First(e => e.Id == breweryId).Links.Beers.Href;
                        response = await client.GetAsync(new Uri(url));
                        stringResponse = await response.Content.ReadAsStringAsync();
                        beers = JsonConvert.DeserializeObject<BeersResponse>(stringResponse);

                        if(beerId > beers.Links.Beers.Count) { Console.WriteLine("Nu exista id-ul berii!"); break; }

                        url = BaseUrl + beers.Links.Beers[beerId - 1].Href;

                        jsonBeerFormat = JsonConvert.SerializeObject(new Beer {Name = newName}, Formatting.Indented);
                        httpContent = new StringContent(jsonBeerFormat, Encoding.UTF8, "application/json");

                        var putResponse = await client.PutAsync(url, httpContent);
                        responseString = await putResponse.Content.ReadAsStringAsync();

                        Console.WriteLine("Status code: " + putResponse.StatusCode);
                        if (putResponse.StatusCode == HttpStatusCode.OK)
                        {
                            Console.WriteLine("Berea a fost edidata: \n" + responseString);
                        }
                        break;

                    //stergerea unei beri
                    case 5:
                        Console.Write("Choose the brewery's id: ");//or name
                        breweryId = int.Parse(Console.ReadLine());
                        if (breweryId > count) { Console.WriteLine("Nu exista id-ul berariei!"); break; }

                        Console.Write("Choose the beer's id: ");
                        beerId = int.Parse(Console.ReadLine());

                        url = BaseUrl + endpoints.Embedded.Brewery.First(e => e.Id == breweryId).Links.Beers.Href;
                        response = await client.GetAsync(new Uri(url));
                        stringResponse = await response.Content.ReadAsStringAsync();
                        beers = JsonConvert.DeserializeObject<BeersResponse>(stringResponse);

                        if (beerId > beers.Links.Beers.Count) { Console.WriteLine("Nu exista id-ul berii!"); break; }

                        url = BaseUrl + beers.Links.Beers[beerId - 1].Href;

                        response = await client.DeleteAsync(new Uri(url));
                        stringResponse = await response.Content.ReadAsStringAsync();
                        var objDel = JsonConvert.DeserializeObject(stringResponse);
                        var jsonObj = JsonConvert.SerializeObject(objDel, Formatting.Indented);
                        Console.WriteLine(jsonObj + "\nStatus code: " + response.StatusCode);

                        break;

                    default:
                        Console.WriteLine("Comanda gresita!");
                        break;
                }

            } while (opt != 0);
        }
    }
}
