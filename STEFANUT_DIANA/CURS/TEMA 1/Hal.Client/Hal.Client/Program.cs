using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Hal.Client;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Hal.Client
{


	class Program
	{

        static void get_Beers()
        {
            beri.RootObject obj2 = new beri.RootObject();
            var client = new HttpClient();
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/beers").Result;

            var data = response.Content.ReadAsStringAsync().Result;

            var obj = JsonConvert.DeserializeObject(data);

            obj2 = JsonConvert.DeserializeObject<beri.RootObject>(data);
        }


        static All.RootObject Get_Beer_From_Link(string link)
        {
            RootObject obj2 = new RootObject();
            string realLink;
            realLink = "http://datc-rest.azurewebsites.net" + link;

            All allObj = new Client.All();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
            var response = client.GetAsync(realLink).Result;

            var data = response.Content.ReadAsStringAsync().Result;

            All.RootObject aro = JsonConvert.DeserializeObject<All.RootObject>(data);

            return aro;
        }

        static Link2.RootObject Get_Beer_From_Link2(string link)
        {
            RootObject obj1 = new RootObject();
            string realLink;
            realLink = "http://datc-rest.azurewebsites.net" + link;

            All allObj = new Client.All();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
            var response = client.GetAsync(realLink).Result;

            var data = response.Content.ReadAsStringAsync().Result;

            Link2.RootObject abc = JsonConvert.DeserializeObject<Link2.RootObject>(data);

            return abc;
        }

        static beri.RootObject Post(int id, string name, string link, int adauga)
        {
            RootObject obj = new RootObject();
            string str_link;
            str_link = "http://datc-rest.azurewebsites.net/beers";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
            var response = client.GetAsync(str_link).Result;

            var data = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(data);

            beri.RootObject abc = JsonConvert.DeserializeObject<beri.RootObject>(data);

            if (adauga == 1)
            {
                beri.RootObject adauga_bere = new beri.RootObject()
                {
                    _embedded = new beri.Embedded()
                    {
                        beer = new List<beri.Beer2>()
                    {
                        new beri.Beer2
                        {
                            Id = id,
                            Name = name,
                            _links = new beri.Links2
                            {
                                self = new beri.Self2
                                {
                                    href = link
                                }
                            }

                        }
                    }
                    }
                };

                abc._embedded.beer.Add(adauga_bere._embedded.beer[0]);
            }
            var jsonOutput = JsonConvert.SerializeObject(abc, Formatting.Indented);
            Console.WriteLine(jsonOutput);

            return abc;

        }

        static void introduce_Bere()
        {
            Console.WriteLine("Bere noua : ");
            Console.WriteLine("Id:");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Link:");
            string link = (Console.ReadLine());

            beri.RootObject bere = Post(id, name, link, 1);
            for (int i = 0; i < bere._embedded.beer.Count(); i++)
            {
                Console.WriteLine(i + " : " + bere._embedded.beer[i].Name);
            }
        }

        static void Main(string[] args)
		{
            RootObject obj1 = new RootObject();

            string link_get = "/breweries";

            All.RootObject abc;
            abc = Get_Beer_From_Link(link_get);
          
            Console.WriteLine(abc._embedded.brewery[0].Name);

        
            int optiune;

            Console.Clear();
            do
            {
               
                Console.Write("\n\n");
                Console.WriteLine("Alege: ");
                Console.WriteLine("   Meniu:\n");
                Console.WriteLine("1. Vizualizare \n");
                Console.WriteLine("2. Adauga bere noua. \n");
                Console.WriteLine("3. Exit. \n\n");
                optiune = Int32.Parse(Console.ReadLine());

                switch (optiune)
                {
                    case 1:
                        Console.WriteLine("Alegeti un tip de bere");

                        for (int i = 1; i < abc._embedded.brewery.Count; i++)
                        {
                            string a = "";
                            a = a + i + ": ";
                            a = a + abc._embedded.brewery[i].Name;

                            Console.WriteLine(a);
                        }

                        int optiune1 = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(optiune1);

                        if (optiune1 < abc._embedded.brewery.Count)
                        {
                            string legatura = abc._embedded.brewery[optiune1-1]._links.beers.href;
                            Console.WriteLine(legatura);
                            Link2.RootObject NewStringBeers;
                            NewStringBeers = Get_Beer_From_Link2(legatura);

                            for (int i = 1; i < NewStringBeers._embedded.beer.Count; i++)
                            {
                                string s = "";
                                s = s + i + ": ";
                                s = s + NewStringBeers._embedded.beer[i].Name;

                                Console.WriteLine(s);
                            }
                        }
                        break;

                    case 2:
                        beri.RootObject beri_robj = Post(0, "0", "0", 0);
                        for (int i = 0; i < beri_robj._embedded.beer.Count(); i++)
                        {
                            Console.WriteLine(i + " : " + beri_robj._embedded.beer[i].Name);
                        }
                        introduce_Bere();
                        break;

                    case 3: break;

                    default:
                        Console.WriteLine("Optiune invalida. \n");
                        break;
                }
            }
            while (optiune != 3);

            Console.ReadLine();
	    }
	}
}
