using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;


namespace Datct1
{
    class Program
    {
        public static List<Bere> ListaBere = new List<Bere>();
        public static List<Berarie> ListaBerarie = new List<Berarie>();
        //public static HttpClient client = new HttpClient();
        public static string LinkPrincipal = "http://datc-rest.azurewebsites.net/";

        static void Main(string[] args)
        {
            int opt = 0, x=0;
            do
            {
                Console.WriteLine("-------------MENIU-----------");
                Console.WriteLine("1.Afisare lista berarie");
                Console.WriteLine("2.Afisare lista bere");
                Console.WriteLine("3.Introducere bere");
                Console.WriteLine("0.Exit!");
                Console.WriteLine("Introduceti optiunea:");
                int.TryParse(Console.ReadLine(), out opt);
                switch (opt)
                {
                    case 1:
                        RunBerarie().Wait(1000);
                        Console.WriteLine("Introduceti id'ul berariei dorite:");
                        int.TryParse(Console.ReadLine(), out x);
                        Berarieinfo(x.ToString()).Wait(1000);
                        break;
                    case 2:
                        RunBere().Wait(1000);
                        Console.WriteLine("Introduceti id'ul berariei dorite:");
                        int.TryParse(Console.ReadLine(), out x);
                        Berarieinfo(x.ToString()).Wait(1000);
                        break;
                    case 3:
                        Bereintroducere();
                        break;
                    case 0:
                        break;
                }
            } while (opt != 0);
        }

        public static string Clear(string data)
        {

            data = data.Replace(",\"_links\":[]", "");
            data = data.Replace("{\"ResourceList\":", "");
            char last = data[data.Length - 1];
            if(last == '}')
            {
                data = data.Remove(data.Length - 1);
            }
            return data;
        }

        public static string Clear_beer(string data)
        {
            data = data.Replace(",\"_links\":[]", "");
            //data = data.Replace("{\"TotalResults\":195,\"TotalPages\":40,\"Page\":1,\"ResourceList\":", "");
            char last = data[data.Length - 1];
            if (last == '}')
            {
                data = data.Remove(data.Length - 1);
            }
            return data;
        }

        public static string Clear_info(string data)
        {
            data = data.Replace(",\"_links\":[]", "");
            data = data.Insert(0, "[");
            data = data.Insert(data.Length, "]");
            return data;
        }

        static async Task RunBerarie()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(LinkPrincipal);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Console.WriteLine("-----Meniu berarii-----\n");
                HttpResponseMessage response = await client.GetAsync("breweries");
                Berarie berarie = await response.Content.ReadAsAsync<Berarie>();
                //use JavaScriptSerializer from System.Web.Script.Serialization
                //string data = await response.Content.ReadAsStringAsync();
                string data2 = await response.Content.ReadAsStringAsync();
                data2 = Clear(data2);
                //Console.WriteLine(data2);
                    //use JavaScriptSerializer from System.Web.Script.Serialization
                JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                    //deserialize to your class
                ListaBerarie = JSserializer.Deserialize<List<Berarie>>(data2);
                //Console.WriteLine(ListaBerarie);
                for (int i = 0; i < ListaBerarie.Count; i++)
                {
                    Console.WriteLine("Id bere:" + ListaBerarie[i].Id);
                    Console.WriteLine("Tip bere:" + ListaBerarie[i].Name);
                    Console.WriteLine("\n");
                }

               
            }
        }

        static async Task RunBere()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(LinkPrincipal);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Console.WriteLine("-----Meniu bere-----\n");
                HttpResponseMessage response = await client.GetAsync("beers");
                Bere bere = await response.Content.ReadAsAsync<Bere>();
                //use JavaScriptSerializer from System.Web.Script.Serialization
                //string data = await response.Content.ReadAsStringAsync();
                string data2 = await response.Content.ReadAsStringAsync();
                string[] dataSplit = data2.Split(new string[] { "\"ResourceList\":" }, StringSplitOptions.RemoveEmptyEntries);
                data2 = Clear_beer(dataSplit[1]);
                //Console.WriteLine(data2);
                //use JavaScriptSerializer from System.Web.Script.Serialization
                JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                //deserialize to your class
                ListaBere = JSserializer.Deserialize<List<Bere>>(data2);
                //Console.WriteLine(ListaBerarie);
                for (int i = 0; i < ListaBere.Count; i++)
                {
                    Console.WriteLine("Id bere:" + ListaBere[i].Id);
                    Console.WriteLine("Tip bere:" + ListaBere[i].Name);
                    Console.WriteLine("\n");
                }


            }
        }

        static async Task Berarieinfo(string nr)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(LinkPrincipal);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Console.WriteLine("-----Informatie berarie-----\n");
                HttpResponseMessage response = await client.GetAsync("breweries/" + nr); ;
                Berarie berarie = await response.Content.ReadAsAsync<Berarie>();
                //use JavaScriptSerializer from System.Web.Script.Serialization
                //string data = await response.Content.ReadAsStringAsync();
                string data2 = await response.Content.ReadAsStringAsync();
                data2 = Clear_info(data2);
                Console.WriteLine(data2);
                //use JavaScriptSerializer from System.Web.Script.Serialization
                JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                //deserialize to your class
                ListaBerarie = JSserializer.Deserialize<List<Berarie>>(data2);
                //Console.WriteLine(ListaBerarie);
                for (int i = 0; i < ListaBerarie.Count; i++)
                {
                    Console.WriteLine("Id berarie:" + ListaBerarie[i].Id);
                    Console.WriteLine("Tip berarie:" + ListaBerarie[i].Name);
                    Console.WriteLine("\n");
                }


            }
        }

            static public void Bereintroducere()
            {
            string numeb = "";
            Console.Clear();
            Console.WriteLine("Introduceti berea dorita:");
            int idBere = 0;
            Console.Write("Id bere:");
            int.TryParse(Console.ReadLine(), out idBere);
            Console.Write("Nume bere:");
            numeb = Console.ReadLine();
            HttpContent httpcont = new FormUrlEncodedContent(new[] { new KeyValuePair<string,string>("Id:",idBere.ToString()),
                        new KeyValuePair<string,string>("Name:",numeb)
                    });
            var HttpCli = new HttpClient();
            var raspuns = HttpCli.PostAsync("http://datc-rest.azurewebsites.net/beers", httpcont);
             }
            static async Task Bereinfo(string nr)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(LinkPrincipal);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Console.WriteLine("-----Informatii bere-----\n");
                    HttpResponseMessage response = await client.GetAsync("beers/" + nr); ;
                    Bere bere = await response.Content.ReadAsAsync<Bere>();
                    //use JavaScriptSerializer from System.Web.Script.Serialization
                    //string data = await response.Content.ReadAsStringAsync();
                    string data2 = await response.Content.ReadAsStringAsync();
                    data2 = Clear_info(data2);
                    Console.WriteLine(data2);
                    //use JavaScriptSerializer from System.Web.Script.Serialization
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                    //deserialize to your class
                    ListaBere = JSserializer.Deserialize<List<Bere>>(data2);
                    //Console.WriteLine(ListaBerarie);
                    for (int i = 0; i < ListaBere.Count; i++)
                    {
                        Console.WriteLine("Id bere:" + ListaBere[i].Id);
                        Console.WriteLine("Tip bere:" + ListaBere[i].Name);
                        Console.WriteLine("\n");
                    }


                }
            }
       
    }
}
