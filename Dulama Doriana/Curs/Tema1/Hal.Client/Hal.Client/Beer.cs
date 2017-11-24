using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hal.Client
{
    class Beer
    {
        
        [JsonProperty(PropertyName = "Id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        //[JsonProperty(PropertyName = "BreweryId")]
        //public int B_ID { get; set; }

        //[JsonProperty(PropertyName = "BreweryName")]
        //public string B_Name { get; set; }

        //[JsonProperty(PropertyName = "StyleId")]
        //public int S_ID { get; set; }

        //[JsonProperty(PropertyName = "StyleName")]
        //public string S_Name { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public Links links { get; set; }

        public void PostBeer()
        {
            //JsonSerializer serializer = new JsonSerializer();
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            Console.WriteLine("----SERIALIZED----");
            Console.WriteLine(json);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://datc-rest.azurewebsites.net/beers");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

        }

        public string toString()
        {
            return ID.ToString();
        }
    }
}
