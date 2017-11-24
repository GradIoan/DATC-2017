using DATC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
	class Program
	{



        void Meniu()
        {
            Console.Write(" \n");
            Console.Write("---------------Meniu--------------\n");
            Console.Write(" \n");
            Console.Write("0.Iesire\n");
            Console.Write("1.Listati toate bauturile\n");
            Console.Write("2.Listati toate berile\n");
            Console.Write("3.Adaugati o noua bere\n");
            Console.Write("Introduceti optiunea:\n");

        }
        void ListBreweries(Data data)
        {
            foreach(var brewery in data.EmbeddedArray.Brewery)
            {
                Console.Write(brewery.Id + ". " + brewery.Name+"\n");
            }
        }
        void ListBeers(BeerData data)
        {
            foreach (var beer in data.Embedded.Beer)
            {
                Console.Write(beer.Id + ". " + beer.Name + "\n");
            }
        }
        void AddBeer(BeerData data)
        {
            Console.Write("Introduceti id:");
            string id = Console.ReadLine();
            Console.Write("Introduceti nume:");
            string nume = Console.ReadLine();

            string href = "/beers/" + id;

            Beer beer = new Beer();
            beer.Href = href;
            data.Link.Beer = new List<Beer>(data.Link.Beer) { beer }.ToArray();
            Beer2 beer2 = new Beer2();
            beer2.Name = nume;
            beer2.Id = id;
            beer2.Link = new _links4();
            beer2.Link.Self = new Self();
            beer2.Link.Self.Href = href;
            data.Embedded.Beer = new List<Beer2>(data.Embedded.Beer) { beer2 }.ToArray();
            String json = JsonConvert.SerializeObject(data);


        }


        static void Main(string[] args)
		{
            Program program = new Program();
            string option;
			var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries").Result;

			var data = response.Content.ReadAsStringAsync().Result;
			Data breweriesObj = JsonConvert.DeserializeObject<Data>(data);

            var beerResponse = client.GetAsync("http://datc-rest.azurewebsites.net/beers").Result;

            var beerData = beerResponse.Content.ReadAsStringAsync().Result;
            BeerData beerObj = JsonConvert.DeserializeObject<BeerData>(beerData);
           // Console.Write(JsonConvert.SerializeObject(beerObj));


            do
            {
                program.Meniu();
                option= Console.ReadLine();
                switch (option) {
                    case "0" : break;
                    case "1" : program.ListBreweries(breweriesObj); break;
                    case "2" : program.ListBeers(beerObj);break;
                    case "3" : program.AddBeer(beerObj); break;
                }

            } while (option!="0");
			Console.ReadLine();
		}
	}
}
