using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static string s_home = "http://datc-rest.azurewebsites.net/breweries/";
        static string[] s_BrNames = new string[20];
        static string s_Beers = "/beers";
        static int[] i_Ids = new int[20];
        static int i_Brs = 0;

        static void Main(string[] args)
        {
            ColectareDate();
            string s_Br = Meniu();
            while(s_Br != "0")
            {
                AfisareBeri(s_Br);
                s_Br = Meniu();
            }
        }

        static private void ColectareDate()
        {
            var client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries/").Result;

            var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject(data);

            JsonTextReader reader = new JsonTextReader(new System.IO.StringReader(data));

            bool b_Switch = false;
            int i_Links = 0;
            while (reader.Read())
            {
                int opt;
                if (reader.Value != null)
                {
                    if (b_Switch == true)
                    {
                        i_Ids[i_Links] = Convert.ToInt32(reader.Value.ToString());
                        b_Switch = false;
                        i_Links++;
                    }
                    if (reader.Value.ToString() == "Id")
                    {
                        b_Switch = true;
                    }
                }

            }
            
            b_Switch = false;
            for (int i_count = 0; i_count < i_Links; i_count++)
            {
                response = client.GetAsync(s_home + i_Ids[i_count].ToString()).Result;

                data = response.Content.ReadAsStringAsync().Result;
                obj = JsonConvert.DeserializeObject(data);

                reader = new JsonTextReader(new System.IO.StringReader(data));

                while (reader.Read())
                {
                    if (reader.Value != null)
                    {
                        if (b_Switch == true)
                        {
                            s_BrNames[i_Brs] = reader.Value.ToString();
                            b_Switch = false;
                            i_Brs++;
                        }
                        if (reader.Value.ToString() == "Name")
                        {
                            b_Switch = true;
                        }
                    }
                }
            }
        }

        static private string Meniu()
        {
            string s_Br;
            Console.WriteLine("The following breweries are available: ");
            for (int i_count = 0; i_count < i_Brs; i_count++)
            {
                Console.WriteLine((i_count + 1).ToString() + ". " + s_BrNames[i_count]);
            }
            Console.WriteLine("a. Add a new beer");
            Console.WriteLine("0. Exit");
            Console.Write("Your choice: ");
            s_Br = Console.ReadLine();
            return s_Br;
        }

        static private void AfisareBeri(string s_Br)
        {
            bool b_check = false;
            if (s_Br == "a")
            {
                HttpPost();
                b_check = true;
            }
            else
            {
                for (int i_count = 0; i_count < i_Brs; i_count++)
                {
                    if (Convert.ToInt32(s_Br) == i_Ids[i_count])
                    {
                        b_check = true;

                        var client = new System.Net.Http.HttpClient();
                        client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
                        var response = client.GetAsync(s_home + i_Ids[i_count] + s_Beers).Result;

                        var data = response.Content.ReadAsStringAsync().Result;
                        var obj = JsonConvert.DeserializeObject(data);

                        JsonTextReader reader = new JsonTextReader(new System.IO.StringReader(data));

                        bool b_Switch = false;
                        Console.Write("Beers:\t\t");
                        while (reader.Read())
                        {
                            if (reader.Value != null)
                            {
                                if (b_Switch == true)
                                {
                                    Console.Write(reader.Value.ToString() + "\t\t");
                                    b_Switch = false;
                                }
                                if (reader.Value.ToString() == "Name")
                                {
                                    b_Switch = true;
                                }
                            }
                        }
                        Console.WriteLine();
                    }
                }
                if (b_check == false)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
        }

        public static void HttpPost()
        {
            string s_Url = "http://datc-rest.azurewebsites.net/beers";
            Console.Write("Choose a beer Id: ");
            i_Ids[i_Brs] = Convert.ToInt32(Console.ReadLine());
            Console.Write("Choose a beer Name: ");
            s_BrNames[i_Brs] = Console.ReadLine();
            Beer B_NewBeer = new Beer(i_Ids[i_Brs], s_BrNames[i_Brs]);
            i_Brs++;
            StringContent SC_Content = new StringContent(JsonConvert.SerializeObject(B_NewBeer));

            using (var v_Client = new HttpClient())
            {
                v_Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                SC_Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                var v_ClientResponse = v_Client.PostAsync(s_Url, SC_Content).Result;
                Console.WriteLine(v_ClientResponse);
            }
        }
    }
}
