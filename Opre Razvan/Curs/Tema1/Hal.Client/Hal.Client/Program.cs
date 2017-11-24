using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Hal.Client
{
    class Program
    {
        static int OPT;

        static void Main(string[] args)
        {
            List<string> breweries_links = new List<string>();
            string api_url = "http://datc-rest.azurewebsites.net";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
            var response = client.GetAsync(api_url + "/breweries").Result;

            var data = response.Content.ReadAsStringAsync().Result;

            Newtonsoft.Json.Linq.JObject obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

            do
            {

                Console.WriteLine("MENIU");
                Console.WriteLine("ALEGE OPTIUNEA DORITA  :");
                Console.WriteLine("1. Berarie");
                Console.WriteLine("2. Tipuri de bere");
                Console.WriteLine("7. Exit");

                OPT = int.Parse(Console.ReadLine());

                switch (OPT)
                {
                    case 1:
                        #region berarie
                        Newtonsoft.Json.Linq.JToken breweries_links_array = obj.First.Last.Last.First;
                        foreach (var item in breweries_links_array)
                        {
                            breweries_links.Add(item.First.First.ToString());
                        }
                        foreach (var item in breweries_links)
                        {
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));

                            response = client.GetAsync(api_url + item.ToString()).Result;

                            data = response.Content.ReadAsStringAsync().Result;
                            obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

                            if (!obj.First.ToString().Equals("\"Message\": \"An error has occurred.\""))
                            {
                                Console.WriteLine(obj.First.Next.First);
                            }
                        }
                        #endregion berarie

                        #region WHILE MENU-MIC
                        do
                        {

                            Console.WriteLine("MENIU");
                            Console.WriteLine("ALEGE OPTIUNEA DORITA  :");
                            Console.WriteLine("1. Beri");
                            Console.WriteLine("7. Exit");

                            OPT = int.Parse(Console.ReadLine());

                            switch (OPT)
                            {
                                case 1:
                                    Console.WriteLine("Dati beraria");
                                    OPT = int.Parse(Console.ReadLine());
                                    #region beri
                                    // Extragem berile de la o berarie specifica.

                                    client.DefaultRequestHeaders.Accept.Clear();
                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
                                    // punem ID-ul berilor !
                                    response = client.GetAsync(api_url + "/breweries/" + OPT + "/beers").Result;

                                    data = response.Content.ReadAsStringAsync().Result;
                                    obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

                                    foreach (var item in obj.Last.First.First.First)
                                    {
                                        Console.WriteLine("Numele berii este : " + item.First.Next.First);
                                        Console.WriteLine("Tipul berii este : " + item.First.Next.Next.Next.Next.Next.First + "\n");
                                    }
                                    #endregion beri

                                    break;
                                case 7:
                                    break;
                                default:
                                    Console.WriteLine("Alegerea nu este corecta");
                                    break;
                            }
                        } while (OPT != 7);
                        #endregion
                        break;
                    case 2:

                        // Extragem tipul de bere
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));

                        response = client.GetAsync(api_url + "/styles").Result;

                        data = response.Content.ReadAsStringAsync().Result;
                        obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

                        Console.WriteLine("\nTipuri de bere");

                        foreach (var item in obj.Last.First.First.First)
                        {
                            Console.WriteLine(item.First.Next.First);
                        }
                        do
                        {

                            Console.WriteLine("MENIU");
                            Console.WriteLine("ALEGE OPTIUNEA DORITA  :");
                            Console.WriteLine("1. Beri");
                            Console.WriteLine("7. Exit");

                            OPT = int.Parse(Console.ReadLine());

                            switch (OPT)
                            {
                                case 1:
                                    Console.WriteLine("Dati stilul");
                                    OPT = int.Parse(Console.ReadLine());
                                    #region beri
                                    // extragem berile de un tip specific

                                    client.DefaultRequestHeaders.Accept.Clear();
                                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
                                    // punem id de stil!

                                    response = client.GetAsync(api_url + "/styles/" + OPT + "/beers").Result;

                                    data = response.Content.ReadAsStringAsync().Result;
                                    obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

                                    Console.WriteLine("\n\nLista berilor de un anumit tip ");

                                    foreach (var item in obj.Last.First.First.First)
                                    {
                                        Console.WriteLine(item.First.Next.First);
                                    }
                                    Console.ReadLine();
                                    #endregion beri

                                    break;
                                case 7:
                                    break;
                                default:
                                    Console.WriteLine("Alegerea nu este corecta");
                                    break;
                            }
                        } while (OPT != 7);
                        break;
                   case 4:
                        /* POST*/
                        break;
                }
            } while (OPT != 7);
            Console.WriteLine();
        }
    }
}

