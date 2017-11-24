using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        static void Main(string[] args)
        {
            int optiune;

            do
            {
                Console.WriteLine("Meniu");
                Console.WriteLine("1.Adaugare bere");
                Console.WriteLine("2.Berarii");
                Console.WriteLine("0.Iesire");
                optiune = Int32.Parse(Console.ReadLine());

                switch(optiune)
                {
                    case 1:
                        Console.WriteLine("Berea adaugata este: \n");
                        string bereadaugata = Console.ReadLine();

                        string bere = "{\"Name\":\"" + bereadaugata + "\"}";
                        string url = "http://datc-rest.azurewebsites.net/beers";
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(bere);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                        break;
                    case 2:
                        string url2 = "http://datc-rest.azurewebsites.net";
                        var client = new HttpClient();
                        client.DefaultRequestHeaders.Add("accept", "application/hal+json");
                        string entryPoint = url2 + "/breweries/";
                        var raspuns = client.GetAsync(entryPoint).Result;
                        var data = raspuns.Content.ReadAsStringAsync().Result;
                        var result = (JObject)JsonConvert.DeserializeObject(data);
                        var berariiInfo = (RootObject1)result;
                        


                        int NumarBerarii = berariiInfo._embedded.brewery.Count();
                        Console.WriteLine("Avem " + NumarBerarii + " berarii.");
                       
                        Console.WriteLine("Beraria aleasa : ");
                        int IdBerarie = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(berariiInfo._embedded.brewery[IdBerarie - 1].Name);

                        break;
                    case 3:
                        Environment.Exit(0);
                        break;

               
                }
            } while (optiune != 0);


    }
    }
}
