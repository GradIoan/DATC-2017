using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hal.Client
{
	class Program
	{
        static void Main(string[] args)
        {
            int optiune = 0;

            Meniu meniu = new Meniu();

            while (true)
            {
                optiune = meniu.getOptiune();
                if (optiune == 1 || optiune == 2)
                    switch (optiune)
                    {
                        case 1:
                            string uri = "http://datc-rest.azurewebsites.net";
                            var client = new HttpClient();
                            client.DefaultRequestHeaders.Add("accept", "application/hal+json");
                            string entryPoint = uri + "/breweries/";
                            var response = client.GetAsync(entryPoint).Result;

                            var data = response.Content.ReadAsStringAsync().Result;
                            var result = (JObject)JsonConvert.DeserializeObject(data);
                            var berarii = (Berarii.BerariiRootObject)result;

                            int totalBerarii = berarii._embedded.brewery.Count();
                            for(int i=0;i<totalBerarii;i++)
                            {
                                try
                                {
                                    Console.WriteLine(i+1 + ". " + berarii._embedded.brewery[i].Name);
                                }catch(Exception e)
                                {

                                }
                            }
                            Console.WriteLine("alege o berarie dintre cele de sus, tasteaza 1/2/.../"  + totalBerarii);
                            int berarieSelectata = int.Parse(Console.ReadLine());
                            try
                            {
                                string urlBerarie = uri + berarii._embedded.brewery[berarieSelectata - 1]._links.beers.href;
                                var responseApi = client.GetAsync(urlBerarie).Result;
                                var dataApi = responseApi.Content.ReadAsStringAsync().Result;
                                var resultApi = (JObject)JsonConvert.DeserializeObject(dataApi);
                                var bereInfo = (TipuriDeBere.RootObject)resultApi;
                                int totalBeri = bereInfo._embedded.beer.Count();

                                Console.WriteLine("beraria contine " + totalBeri + " beri :");
                                for (int j = 0; j < totalBeri; j++)
                                    Console.WriteLine(j + 1 + ". " + bereInfo._embedded.beer[j].Name);

                                int bereSelectata = int.Parse(Console.ReadLine());
                                Console.WriteLine("selectati o bere pentru a-i vedea link-urile");
                                int totalLinks = bereInfo._links.beer.Count();
                                Console.WriteLine("berea selectata are "+ totalLinks+ "alegeti un link ");
                                int linkSelectat = int.Parse(Console.ReadLine());
                                Console.WriteLine(bereInfo._links.beer[bereSelectata - 1].href);
                            }catch(Exception e)
                            {
                                Console.WriteLine("nu sunt disponibile informatiile");
                            }
                            break;
                        case 2:
                            client = new HttpClient();
                            Console.WriteLine("Intorduceti numele berii");
                            string nume = Console.ReadLine();

                            string bere = "{\"Name\":\"" + nume + "\"}";
                            var postResponse = client.PostAsJsonAsync("http://datc-rest.azurewebsites.net/beers", bere);

                            break;

                    }
                else
                {
                    Console.WriteLine("Optiune incorecta");
                }

                Console.WriteLine("");
            }
        }
	}
}
