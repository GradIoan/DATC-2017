using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1
{
    public class DataHandler
    {
        public static string URL = "http://datc-rest.azurewebsites.net/breweries";

        //A function that converts data from a given url to json.
        public static JObject Get_Json(string url)
        {
            JObject json_data = null;
            string data = string.Empty;
            var request = WebRequest.Create(url);

            request.ContentType = "application/hal+json";
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    data = sr.ReadToEnd();
                }
            }

            json_data = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(data);

            return json_data;
        }

        public static List<Berarie> GetBreweriesData(JObject obj)
        {
            List<Berarie> lista_berarii = new List<Berarie>();

            try
            {
                int i = 0;
                var brewery = obj["_embedded"]["brewery"];

                while (brewery[i] != null)
                {
                    Berarie br = new Berarie();
                    br.Nume = brewery[i]["Name"].ToString();
                    br.ID = Convert.ToInt16(brewery[i]["Id"]);
                    if (brewery[i]["_links"]["beers"]["href"] != null)
                    {
                        br.Link = brewery[i]["_links"]["beers"]["href"].ToString();
                    }
                    i++;
                    lista_berarii.Add(br);
                    br = null;
                }
            }
            catch (Exception e) { }

            return lista_berarii;

        }

        public static List<Bere> GetBeersData(JObject obj)
        {
            List<Bere> lista_beri = new List<Bere>();

            try
            {
                var beers = obj["_embedded"]["beer"];
                int i = 0;
                while (beers[i] != null)
                {
                    try
                    {
                        Bere b = new Bere();

                        if (beers[i]["Id"] != null)
                            b.Id = Convert.ToInt32(beers[i]["Id"]);
                        if (beers[i]["Name"] != null)
                            b.Nume = beers[i]["Name"].ToString();
                        if (beers[i]["BreweryId"] != null)
                            b.Id_Berarie = Convert.ToInt32(beers[i]["BreweryId"]);
                        if (beers[i]["BreweryName"] != null)
                            b.NumeBerarie = beers[i]["BreweryName"].ToString();
                        if (beers[i]["StyleId"] != null)
                            b.StyleID = Convert.ToInt32(beers[i]["StyleId"]);
                        if (beers[i]["StyleName"] != null)
                            b.StyleName = beers[i]["StyleName"].ToString();
                        if (beers[i]["_links"]["style"]["href"] != null)
                            b.StyleLink = beers[i]["_links"]["style"]["href"].ToString();
                        if (beers[i]["_links"]["self"]["href"] != null)
                            b.SelfLink = beers[i]["_links"]["self"]["href"].ToString();

                        try
                        {
                            if (beers[i]["_links"]["review"]["href"] != null)
                                b.ReviewLink = beers[i]["_links"]["review"]["href"].ToString();
                        }
                        catch { }

                        lista_beri.Add(b);
                    }
                    catch { }
                    i++;
                }
            }
            catch { }

            return lista_beri;
        }


        public static void Adauga_Bere()
        {
            Bere b = new Bere();
            string url = URL;
            url.Substring(10);
            url += "\beers";    
            
            Console.WriteLine("Introduce ID: ");
            b.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Introduce numele: ");
            b.Nume = Console.ReadLine();
            StringContent content = new StringContent(JsonConvert.SerializeObject(b));
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var clientResponse = client.PostAsync(url, content).Result;
            Console.WriteLine(clientResponse);
        }


    }
}
