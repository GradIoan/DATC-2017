using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace Hal.Client
{
	class Program
	{
		static void Main(string[] args)
		{
            List<string> breweries_links = new List<string>();

            // stuff for http request
            string api_url = "http://datc-rest.azurewebsites.net";
            HttpClient client = new HttpClient(); ;
            HttpResponseMessage response;
            Newtonsoft.Json.Linq.JObject obj;

            string data;

            string option="abcdefg";

            get_breweries_hrefs();

            while ( !option.Equals("10"))
            {
                main_menu();

                // we want breweries names
                if (option.Equals("1"))
                {
                    breweries_menu();

                    if (Int32.Parse(option) <= breweries_links.Count)
                    {
                        print_beers_from_brewery(option);
                    }
                }

                //we want list of beer styles
                if (option.Equals("2"))
                {
                    beer_styles_menu();

                    if (Int32.Parse(option) <= 9)
                    {
                        print_beers_of_style(option);
                    }
                }

                if (option.Equals("3"))
                {
                    post_new_beer();
                }
            }


            void main_menu()
            {
                    Console.Clear();
                    Console.WriteLine("1.Print breweries.( inside you can see the beers from each brewery )");
                    Console.WriteLine("2.Print beer styles. ( inside you can see beers from each style )" );
                    Console.WriteLine("3.Push a new beer.");
                    Console.WriteLine("Press Enter to go back to main menu.");
                    Console.WriteLine("10.Close.\n\n");

                    option = Console.ReadLine();              
            }

            void breweries_menu()
            {
                print_breweries_names();

                Console.WriteLine();
                Console.WriteLine("Write the brewery order number if you want to see the list of beers.");
                Console.WriteLine("Press Enter to go back to main menu.");
                Console.WriteLine("10.Close.\n\n");

                option = Console.ReadLine();

                if (option.Equals(""))
                    option = "99";

            }

            void beer_styles_menu()
            {
                print_beer_styles();

                Console.WriteLine();
                Console.WriteLine("Write the style order number if you want to see the list of beers.");
                Console.WriteLine("Press Enter to go back to main menu.");
                Console.WriteLine("10.Close.\n\n");

                option = Console.ReadLine();

                if (option.Equals(""))
                    option = "99";
            }

            void post_new_beer()
            {
                WebClient post_client = new WebClient();

                Console.Write("New beer name : ");
                string name = Console.ReadLine();

                byte[] post_response =
                post_client.UploadValues("http://datc-rest.azurewebsites.net/beers", new NameValueCollection()
                {
                        { "name", name }
                });

                string result = System.Text.Encoding.UTF8.GetString(post_response);
                Console.WriteLine("\nBeer added !");

                Console.WriteLine("\nPress Enter to go back to main menu.");

                Console.Read();
                
            }

            void get_breweries_hrefs()
            {
                // the base api call to get breweries hrefs
                client.DefaultRequestHeaders.Accept.Clear();
                // we set the special header to extend hrefs
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
                // response of the http request
                response = client.GetAsync(api_url + "/breweries").Result;
                // data is the json object, but type string
                data = response.Content.ReadAsStringAsync().Result;


                obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);


                //  Console.WriteLine(obj);
                // Console.Read();

                // we go to the breweries array
                //        First        Last              Last        First
                // obj => links => self and brewery = > brewery => breweries array
                Newtonsoft.Json.Linq.JToken breweries_links_array = obj.First.Last.Last.First;

                // we extract each brewery link ( ex :  /breweries/2, /breweries/3 etc )
                foreach (var item in breweries_links_array)
                {
                    breweries_links.Add(item.First.First.ToString());
                    //Console.WriteLine(item.First.First);
                }
            }

            void print_breweries_names()
            {
                Console.WriteLine();
                Console.WriteLine("Breweries list : ");


                // here we extract the breweries names
                foreach (var item in breweries_links)
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));

                    response = client.GetAsync(api_url + item.ToString()).Result;

                    data = response.Content.ReadAsStringAsync().Result;
                    obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

                    if (!obj.First.ToString().Equals("\"Message\": \"An error has occurred.\""))
                    {
                        // we go to the name string
                        //               First                       Next       First
                        //obj => array of details ( id,name etc) = > Name = > Name string
                        Console.WriteLine(obj.First.Next.First);
                    }
                }
            }
       
            void print_beers_from_brewery(string opt)
            {
                // here we extract the beers from the specific brewery

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
                                                                 // here you put the brewery id !
                                                                 //  ||
                response = client.GetAsync(api_url + "/breweries/" + opt  + "/beers").Result;

                data = response.Content.ReadAsStringAsync().Result;
                obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

                foreach (var item in obj.Last.First.First.First)
                {
                    //the beer name
                    Console.WriteLine("The beer name is : " + item.First.Next.First);

                    //beer style name
                    Console.WriteLine("The beer style is : " + item.First.Next.Next.Next.Next.Next.First + "\n");
                }

                Console.WriteLine("Press enter to go back to main menu");
                Console.Read();
            }

            void print_beer_styles()
            {
                Console.WriteLine();
                Console.WriteLine("\nThese are the beer styels : \n");

                // here we extract the beer styles 

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));

                response = client.GetAsync(api_url + "/styles").Result;

                data = response.Content.ReadAsStringAsync().Result;
                obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);              

                foreach (var item in obj.Last.First.First.First)
                {
                    Console.WriteLine(item.First.Next.First);
                }
            }

            void print_beers_of_style(string opt)
            {
                // here we extract the beers of a specific style 

                Console.WriteLine();
                Console.WriteLine("Beers  : ");


                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/hal+json"));
                                                              // here you put the style id !
                                                              //  ||
                response = client.GetAsync(api_url + "/styles/" + opt + "/beers").Result;

                data = response.Content.ReadAsStringAsync().Result;
                obj = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(data);

                foreach (var item in obj.Last.First.First.First)
                {
                    Console.WriteLine(item.First.Next.First);
                }

                Console.WriteLine();
                Console.WriteLine("Press enter to go back to main menu");
                Console.Read();
            }

            //Console.ReadLine();
		}
	}
}
