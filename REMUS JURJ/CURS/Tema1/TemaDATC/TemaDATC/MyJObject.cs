using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TemaDATC
{
    class MyJObject
    {
        // GET DATA FROM URL
        public static string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/hal+json";
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                }
            }
            return "Error";
        }

        public static void POST(string url, int Id, string Name)
        {
            string newUrl = url.Substring(0,34) +"/beers";
            Beers b = new Beers(Id, Name);

            
            StringContent content = new StringContent(JsonConvert.SerializeObject(b));


            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            var clientResponse = client.PostAsync(newUrl, content).Result;
            Console.WriteLine(clientResponse);
        }
    }
}
