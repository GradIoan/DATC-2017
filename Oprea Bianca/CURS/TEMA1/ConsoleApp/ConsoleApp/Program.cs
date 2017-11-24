using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace ConsoleApp
{
    class Program
    {
        static HttpClient client = new HttpClient();
         static void Main(string[] args)
        {
            char opt = ' ';
            do
            {
                Console.WriteLine("\n Meniu");
                Console.WriteLine("===============================");
                Console.WriteLine("v --- Vizualizeaza stilurile de bere\n");
                Console.WriteLine("a --- Adauga o bere noua\n");
                Console.WriteLine("o --- Acceseaza o berarie\n");
                Console.WriteLine("e --- Exit\n");
                Console.WriteLine("Optiune: ");
                opt = Convert.ToChar( Console.ReadLine());
                switch (opt)
                {
                    case 'v':
                        GetRequest("http://datc-rest.azurewebsites.net/breweries");
                        alege_stil();
                        break;
                    case 'a':
                        PostRequest("http://datc-rest.azurewebsites.net/beers");
                        Console.WriteLine("Apasati orice tasta");
                        Console.ReadKey();
                        break;
                    case 'o':
                        alege_berarie();
                        break;
                    case 'e':
                        break;
                    default:
                        Console.WriteLine("Optiunea introdusa nu este valabila!");
                        break;
                }

            } while (opt !='e');
        }
        static void GetRequest(string uri)
        {
            var response = client.GetAsync(uri).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject(data);

            HttpContent content = response.Content;
            HttpContentHeaders head = content.Headers;
            HttpRequestHeaders req = response.RequestMessage.Headers;
            req.Add("Accept", "application/hal+json");
            deserializare(data);
        }
        private static void deserializare(string json)
        {
            var berarii = JsonConvert.DeserializeObject<Berarii_Stil>(json);
            Console.WriteLine("Stiluri berarii\n");
            Console.WriteLine("\nId     Nume\n");
            try
            {
                foreach (Berarii_Stil.resrc resurse in berarii.ResourceList)
                {

                    Console.WriteLine(resurse.Id + " - " + resurse.Name);
                    Console.WriteLine(resurse.links);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

        }
        private static void alege_stil()
        {
            int optiune = 0;
            do
            {
                Console.WriteLine("\n1 --- Afiseaza berile specifice stilului ales\n");
                Console.WriteLine("0 --- Exit\n");
                Console.WriteLine("Optiune: ");
                optiune = Convert.ToInt32(Console.ReadLine());
                switch (optiune)
                {
                    case 1:
                        Console.WriteLine("Id-ul stilului: ");
                        long id_stil = Convert.ToInt32(Console.ReadLine());
                        info_stil_beri(id_stil);
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Optiunea introdusa nu este valabila!");
                        break;
                }
            } while (optiune != 0);
        }
        private static void info_stil_beri(long id)
        {
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/styles" + "/" + id + "/beers").Result;
            HttpRequestHeaders req = response.RequestMessage.Headers;
            req.Add("Accept", "application/hal+json");
            var data = response.Content.ReadAsStringAsync().Result;
            try
            {
                var bere = JsonConvert.DeserializeObject<Beri>(data);
                Console.WriteLine("\nBeri specifice stilului ales\n");
                foreach (Beri.resrc resurse in bere.ResourceList)
                {
                    Console.WriteLine("\t   Id - " + resurse.Id);
                    Console.WriteLine("\t - Name - " + resurse.Name);
                    Console.WriteLine("\t - BreweryID - " + resurse.BreweryId);
                    Console.WriteLine("\t - BreweryName - " + resurse.BreweryName);
                    Console.WriteLine("\t - StyleId - " + resurse.StyleId);
                    Console.WriteLine("\t - StyleName - " + resurse.StyleName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        async static void PostRequest(string url)
        {
            IEnumerable<KeyValuePair<string, string>> beers = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Name","Heineken")
            };

            HttpContent c = new FormUrlEncodedContent(beers);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, c);
            HttpContent content = response.Content;
            string rsp = await content.ReadAsStringAsync();
            Console.WriteLine(rsp);

            if (response.IsSuccessStatusCode)
                Console.WriteLine("post cu succes!");
            else
                Console.WriteLine("post fara succes!");
        }

        private static void alege_berarie()
        {
            int id, optiune = 0;
            Console.WriteLine("Id-ul berariei: ");
            id = Convert.ToInt32(Console.ReadLine());
            long br_id = info_berarie(id);
                do
                {
                    Console.WriteLine("\n1 --- Afiseaza berile berariei\n");
                    Console.WriteLine("2 --- Acceseaza beraria urmatoare\n");
                    Console.WriteLine("3 --- Acceseaza beraria precedenta\n");
                    Console.WriteLine("0 --- Exit\n");
                    Console.WriteLine("Optiune: ");
                    optiune = Convert.ToInt32(Console.ReadLine());
                    switch (optiune)
                    {
                        case 1:
                            info_beri(br_id);
                            break;
                        case 2:
                            long _br_id = info_berarie(id + 1);
                            if (_br_id == 0)
                            {
                                while ((id + 1) > 30 && _br_id == 0) 
                                {
                                    _br_id = info_berarie(id + 1);
                                }
                            }
                            break;
                        case 3:
                            long br_idd = info_berarie(id - 1);
                            if (br_idd == 0)
                            {
                                while ((id - 1) != 0 && br_idd == 0) 
                                {
                                    _br_id = info_berarie(id - 1);
                                }
                            }
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Optiunea aleasa nu este valabila!");
                            break;
                    }
                } while (optiune != 0);
            }
        
        private static long info_berarie(long id)  //berarie
        {
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries" + "/" + id).Result;
            HttpRequestHeaders req = response.RequestMessage.Headers;
            req.Add("Accept", "application/hal+json");
            var data = response.Content.ReadAsStringAsync().Result;
            try
            {
                var berarie = JsonConvert.DeserializeObject<Berarie>(data);
                Console.WriteLine("\nId      Name\n");
                Console.WriteLine(berarie.Id + " - " + berarie.Name);
                return berarie.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return -1;
            }
        }
        private static void info_beri(long id)
        {
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries" + "/" + id + "/beers").Result;
            HttpRequestHeaders req = response.RequestMessage.Headers;
            req.Add("Accept", "application/hal+json");
            var data = response.Content.ReadAsStringAsync().Result;
            try
                {
                    var bere = JsonConvert.DeserializeObject<Beri>(data);
                    foreach (Beri.resrc resurse in bere.ResourceList)
                    {
                        Console.WriteLine("\t   Id - " + resurse.Id);
                        Console.WriteLine("\t - Name - " + resurse.Name);
                        Console.WriteLine("\t - BreweryID - " + resurse.BreweryId);
                        Console.WriteLine("\t - BreweryName - " + resurse.BreweryName);
                        Console.WriteLine("\t - StyleId - " + resurse.StyleId);
                        Console.WriteLine("\t - StyleName - " + resurse.StyleName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
           
        }
        
    }
