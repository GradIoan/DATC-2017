using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using System.Net;

namespace Hal.Client
{
    class Program
    {
        static List<string> berarii_links = new List<string>();
        // stuff for http request
        static string api_url = "http://datc-rest.azurewebsites.net";
        static HttpClient client = new HttpClient();
        static HttpResponseMessage response;
        static Newtonsoft.Json.Linq.JObject obj;
        static string data;
        static string opt;
        private static HttpContent bere;

        static void Main(string[] args)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));


            var response = client.GetAsync(api_url + "/breweries").Result;

            var data = response.Content.ReadAsStringAsync().Result;

            Newtonsoft.Json.Linq.JObject obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);
            //mergem la vectorul cu berarii
            Newtonsoft.Json.Linq.JToken berarii_links_array = obj.First.Last.Last.First;

            // extragem fiecare link de berarie si il adaugam in lista
            foreach (var item in berarii_links_array)
            {
                berarii_links.Add(item.First.First.ToString());

            }


            //MENIU
            do
            {
                Console.WriteLine("\nMENIUL PRINCIPAL:\n");
                Console.WriteLine("Pentru a vizualiza berariile apasati tasta 1");
                Console.WriteLine("Pentru a vizualiza stilurile de bere apasati tasta 2");
                Console.WriteLine("Pentru a iesi apasati 0");
                Console.WriteLine("\n Alegeti optiunea dorita!");
                opt = Console.ReadLine();
                switch (opt)
                {
                    case "1":
                        berarii();
                        meniu_berarie();
                        break;
                    case "2":
                        stiluri_bere();
                        meniu_stiluri();
                        break;
                    default:
                        Console.WriteLine("Alegeti alta optiune, cea introdusa de dumneavoastra nu exista!!!!!!!!!\n");
                        break;
                }
                Console.Clear();
            } while (!opt.Equals("e"));
            //POST();

            Console.ReadKey();
        }
        static public void meniu_berarie()
        {
            do
            {
                Console.WriteLine("                                    Daca doriti sa vizualizati berile dintr-o anumita berarie apasati tasta 1 ");
                Console.WriteLine("                                    Pentru a reveni la meniul principal apasati tasta 0\n");
                opt = Console.ReadLine();
                switch (opt)
                {
                    case "1":
                        Console.WriteLine("Introduceti ID-ul berariei pentru care solicitati lista de beri");
                        opt = Console.ReadLine();
                        beri_berarie(opt);
                        break;
                    default:
                        Console.WriteLine("Alegeti alta optiune, cea introdusa de dumneavoastra nu exista!!!!!!!!!");
                        break;
                }
            } while (!opt.Equals("0"));
        }
        static public void meniu_stiluri()
        {
            do
            {
                Console.WriteLine("                                    Daca doriti sa vizualizati berile dintr-un anumit stil apasati tasta 1 ");
                Console.WriteLine("                                    Pentru a reveni la meniul principal apasati tasta 0\n");
                opt = Console.ReadLine();
                switch (opt)
                {
                    case "1":
                        Console.WriteLine("Introduceti ID-ul stilului de bere pentru care solicitati lista de beri");
                        opt = Console.ReadLine();
                        beri_stil(opt);
                        break;
                    default:
                        Console.WriteLine("Alegeti alta optiune, cea introdusa de dumneavoastra nu exista!!!!!!!!!");
                        break;
                }
            } while (!opt.Equals("0"));
        }
        static public void beri_berarie(string opt)
        {
            Console.WriteLine("LISTA BERI PENTRU BERARIA ALEASA");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
            //extrag berile pentru beraria specificata in optiune
            response = client.GetAsync(api_url + "/breweries/" + opt + "/beers").Result;

            data = response.Content.ReadAsStringAsync().Result;
            obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

            // Console.WriteLine(obj);

            foreach (var item in obj.Last.First.First.First)
            {
                try
                {
                    Console.WriteLine("ID bere:    " + item.First.First);
                    Console.WriteLine("Nume bere : " + item.First.Next.First);
                    Console.WriteLine("Stil bere : " + item.First.Next.Next.Next.Next.Next.First + "\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ID-ul berarii pentru care solicitati lista de beri este invalid!!!! \n");

                }

            }
        }

        static public void beri_stil(string opt)
        {
            Console.WriteLine("LISTA BERI PENTRU STILUL DE BERE ALES\n");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
            //extrag berile pentru stilul de bere cerut in optiune
            response = client.GetAsync(api_url + "/styles/" + opt + "/beers").Result;

            data = response.Content.ReadAsStringAsync().Result;
            obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

            foreach (var item in obj.Last.First.First.First)
            {
                try
                {
                    Console.WriteLine("ID bere:" + item.First.First);
                    Console.WriteLine("Nume bere:" + item.First.Next.First);
                    Console.WriteLine("ID berarie in care apare:" + item.First.Next.Next.First);//breweryid
                    Console.WriteLine("Nume berarie in care apare:" + item.First.Next.Next.Next.First);//breweryname
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ID-ul stilului pe care l-ati introdus nu este valid!");
                }
            }
        }

        static public void stiluri_bere()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));

            response = client.GetAsync(api_url + "/styles").Result;

            data = response.Content.ReadAsStringAsync().Result;
            obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);
            Console.WriteLine("\nLISTA STILURI DE BERE:\n");
            foreach (var item in obj.Last.First.First.First)
            {
                Console.WriteLine("ID stil bere:   " + item.First.First);
                Console.WriteLine("Nume stil bere: " + item.First.Next.First + "\n");
            }
        }

        static public void berarii()
        {

            Console.WriteLine("\nLISTA BERARIILOR: \n");

            foreach (var item in berarii_links)
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));

                response = client.GetAsync(api_url + item.ToString()).Result;

                data = response.Content.ReadAsStringAsync().Result;
                obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

                if (!obj.First.ToString().Equals("\"Message\": \"An error has occurred.\""))
                {

                    Console.WriteLine("ID berarie:   " + obj.First.First);
                    Console.WriteLine("Nume berarie: " + obj.First.Next.First + "\n");
                }
            }
        }

        static public void POST()
        {
           
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["Id"] = "1";
                data["Name"] = "test";

                var response = wb.UploadValues("http://datc-rest.azurewebsites.net/beers", "POST", data);
                Console.WriteLine(response.ToString());

            }
        }
    }
}