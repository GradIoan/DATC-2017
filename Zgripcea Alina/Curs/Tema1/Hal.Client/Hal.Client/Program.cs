/* Citirea unui json si parcurgerea acestuia cu scopul de a afisa elementele ce se gasesc in acesta
 * adaugarea unui nou element intr-un json */

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

        static void getBeers() // am facut o functie din ceea ce se gaseste in exemplul de la curs
        {
            beers.RootObject obj1 = new beers.RootObject();
            var client = new HttpClient();
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/beers").Result;

            var data = response.Content.ReadAsStringAsync().Result;

            var obj = JsonConvert.DeserializeObject(data);

            obj1 = JsonConvert.DeserializeObject<beers.RootObject>(data);
        }


        static hal.RootObject getBeerFromStr(string link)
        {
            RootObject obj1 = new RootObject();
            string realLink;
            realLink = "http://datc-rest.azurewebsites.net" + link; // folosesc in acest stil pentru ca vreau sa pot sa adaug
                                                                // linkurile fara sa fie nevoie sa fac mai multe functii (pentru json-urile de acelasi tip)

            hal halObj = new Client.hal();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
            var response = client.GetAsync(realLink).Result;

            var data = response.Content.ReadAsStringAsync().Result;

            hal.RootObject x = JsonConvert.DeserializeObject<hal.RootObject>(data);

            //Console.WriteLine(data);
            return x;
        }

        static SecondLink.RootObject getBeerFromStr2(string link) // al doilea link, adica cel in care se gasesc berile are o structura
                                    // diferita de cea a primului link, de aceea avem nevoie de o noua clasa
        {
            RootObject obj1 = new RootObject();
            string realLink;
            realLink = "http://datc-rest.azurewebsites.net" + link;

            hal halObj = new Client.hal();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
            var response = client.GetAsync(realLink).Result;

            var data = response.Content.ReadAsStringAsync().Result;

            SecondLink.RootObject x = JsonConvert.DeserializeObject<SecondLink.RootObject>(data);

           // Console.WriteLine(data);
            return x;
        }

        static beersInfo.RootObject getBeerInfo(string link)
        {
            RootObject obj1 = new RootObject();
            string realLink;
            realLink = "http://datc-rest.azurewebsites.net" + link;

            hal halObj = new Client.hal();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
            var response = client.GetAsync(realLink).Result;

            var data = response.Content.ReadAsStringAsync().Result;

            //Console.WriteLine(data); // afisarea json-ului ajuta la determinarea clasei cu ajutorul json@csharp.com
            beersInfo.RootObject x = JsonConvert.DeserializeObject<beersInfo.RootObject>(data);

            // Console.WriteLine(data);
            return x;
        }

        static beers.RootObject postJson(int id, string name, string link, int add)
        {
            RootObject obj1 = new RootObject();
            string realLink;
            realLink = "http://datc-rest.azurewebsites.net/beers";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
            var response = client.GetAsync(realLink).Result;

            var data = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(data);

            beers.RootObject x = JsonConvert.DeserializeObject<beers.RootObject>(data);

            if (add == 1)
            {
                beers.RootObject addBeer = new beers.RootObject() // un fel de clasa x = new clasa(); dar pentru clase imbricate
                {
                    _embedded = new beers.Embedded()
                    {
                        beer = new List<beers.Beer2>() // cand am incercat sa fac o instantiere cu add am avut o problema 
                    {                                   // legat de instantierea listei, deci solutia a fost crearea unei astfel de structuri in care sa aiba loc
                        new beers.Beer2                 //instantierea explicita a fiecarui element. Astfel pot sa creez un element al listei fara probleme.
                        {
                            Id = id,
                            Name = name,
                            _links = new beers.Links2
                            {
                                self = new beers.Self2
                                {
                                    href = link
                                }
                            }

                        }
                    }
                    }
                };

                x._embedded.beer.Add(addBeer._embedded.beer[0]);
            }
            var jsonOutput = JsonConvert.SerializeObject(x, Formatting.Indented);
            Console.WriteLine(jsonOutput);

            return x;
        }

        static void introdBere()
        {
            Console.WriteLine("Introduceti datele pentru a adauga o bere : ");
            Console.WriteLine("Id:");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Link:");
            string link = (Console.ReadLine());

            beers.RootObject b = postJson(id, name, link, 1); // fac introducerea in json a valorilor date de la tastatura

            for (int i = 0; i < b._embedded.beer.Count(); i++)
            {
                Console.WriteLine(i + " : " + b._embedded.beer[i].Name);
            }
        }

        static void Main(string[] args)
		{
            RootObject obj1 = new RootObject();
            int opt = -10;
            int meniu = 0;
            string linkGet = "/breweries";

            hal.RootObject x;
            x = getBeerFromStr(linkGet);
          
            Console.WriteLine(x._embedded.brewery[0].Name);

           // while (opt != -10)
           // {
            int men1 = 0;
            do
            {
                do
                {
                    Console.WriteLine("Alegeti actiunea ce doriti sa o efectuati:");
                    Console.WriteLine("1 : Parcurge beri");
                    Console.WriteLine("2 : Adauga beri");
                    Console.WriteLine("3 : Exit");
                    men1 = Int32.Parse(Console.ReadLine());
                } while (men1 == 0 || men1<1 || men1>3); // oblig utilizatorul sa introduca o optiune valida

                if (men1 == 3)
                {
                    Console.WriteLine("Exit!");
                    return;
                }else
                if (men1 == 2) // Am ales sa adaug beri
                {
                    beers.RootObject a = postJson(0, "0", "0", 0); // obtinerea json-ului nemodificat

                    for (int i = 0; i < a._embedded.beer.Count(); i++)
                    {
                        Console.WriteLine(i + " : " + a._embedded.beer[i].Name);
                    }

                    introdBere();
                    Console.WriteLine("0 : Inapoi");
                    Console.WriteLine("1 : Exit");
                    Console.WriteLine("2 : Introduceti alta bere");

                    int men2 = Int32.Parse(Console.ReadLine());

                    if (men2 == 1)
                    {
                        Console.WriteLine("Exit");
                        return;
                    }
                    else
                    {
                        men1 = men2;
                    }
                }else
                if (men1 == 1)
                {
                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Alegeti un tip de bere din cele de mai jos: ");

                        for (int i = 0; i < x._embedded.brewery.Count; i++)
                        {
                            string s = "";
                            int i2 = i;
                            s = s + (i2 + 1) + ": ";
                            s = s + x._embedded.brewery[i].Name;

                            Console.WriteLine(s);
                        }
                        Console.WriteLine("0 : Exit");
                        Console.WriteLine();

                        opt = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(opt);
                        if (opt == 0 || opt < 0)
                        {
                            return;
                        }
                        else
                            if ((opt < x._embedded.brewery.Count) && (x._embedded.brewery[opt-1]._links != null))
                            {
                                string link1 = x._embedded.brewery[opt-1]._links.beers.href;
                                Console.WriteLine(link1);
                                SecondLink.RootObject NewStringBeers;
                                NewStringBeers = getBeerFromStr2(link1);

                                for (int i = 0; i < NewStringBeers._embedded.beer.Count; i++)
                                {
                                    string s = "";
                                    s = s + i + ": ";
                                    s = s + NewStringBeers._embedded.beer[i].Name;

                                    Console.WriteLine(s);
                                }
                                Console.WriteLine();
                                Console.WriteLine("Alegeti berea despre care doriti informatii:");
                                int informatii = Int32.Parse(Console.ReadLine());

                                if (informatii < NewStringBeers._embedded.beer.Count)
                                {
                                    beersInfo.RootObject info = getBeerInfo(NewStringBeers._embedded.beer[informatii]._links.brewery.href);
                                    Console.WriteLine("ID : " + info.Id + " Name " + info.Name);
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine("Valoarea nu este dintre cele din lista\n");
                                    Console.WriteLine();
                                }

                        }
                        else
                        {
                            if (opt >= x._embedded.brewery.Count)
                            {
                                Console.WriteLine("In afara optiunilor\n");
                                Console.WriteLine();
                            }
                            else if (opt == -1)
                            {
                                Console.WriteLine("Exit!\n");
                                Console.WriteLine();

                            }
                        }
                    } while (opt != -1);
                }
                Console.WriteLine("Gata!");

            } while (men1 != 3);
           
            Console.ReadLine();
	    }
	}
}
