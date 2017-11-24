using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Net.Http;

namespace ConsoleApplication1
{
    class Resource
    {
        public string id { get; set; }
        public string name { get; set; }
        public string links { get; set; }
    }

    class R_list
    {
        public List<Resource> ResourceList = new List<Resource>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries").Result;

            var data = response.Content.ReadAsStringAsync().Result;

            //var obj = JsonConvert.DeserializeObject(data);
            //HttpRequestHeaders req = response.RequestMessage.Headers;
            // req.Add("Accept", "application/hal+json");
            //Console.WriteLine(obj);
            JavaScriptSerializer serializer = new JavaScriptSerializer();//serilizatorul 

            //deserializare
            R_list lista = (R_list)serializer.Deserialize(data, typeof(R_list));

            Console.WriteLine(data);

            foreach(Resource res in lista.ResourceList)
            {
                Console.WriteLine(res.id);
                Console.WriteLine(res.name);
                Console.WriteLine(res.links);
            }

            //serializare
            string json = serializer.Serialize(lista);

            Console.WriteLine(json);
            Console.ReadLine();
        }
    }
}
