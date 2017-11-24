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


namespace TrutiuBiancaTema1
{
    
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static string datcLink = "http://datc-rest.azurewebsites.net/breweries";

        static void Main(string[] args)
        {
            int opt=1;
            string optString;           
            int optBeweryDetails = 0;            
            string data = loadData(datcLink);

            /*Getting Breweries part*/
            string[] separator = {"brewery"};
            string[] dataExtracted = data.Split(separator, StringSplitOptions.None);          
            
            /*Create the brewery list*/
            string beweryDetailsData = createJsonString(dataExtracted[2]);
            List<BeweryDetails> beweryDetailsList = JsonConvert.DeserializeObject<List<BeweryDetails>>(beweryDetailsData);
            
            /*Keep Menu alive*/
            do
            {
                /*Display Breweries*/
                if (optBeweryDetails == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Choose one: ");
                    foreach (BeweryDetails bwD in beweryDetailsList)
                    {
                        Console.WriteLine(bwD.Id + " Bewery");
                    }
                    Console.WriteLine((beweryDetailsList.Count()+1).ToString()+" Post one Brewery");
                    Console.WriteLine("0 Exit");
                    /*Choose next action*/
                    optString = Console.ReadLine();
                    opt = int.Parse(optString);
                }
                                
                if(opt!= 0 && opt< beweryDetailsList.Count()+1)
                {
                    /*One brewery chosen => Show More*/
                    Console.Clear();
                    Console.WriteLine("ID: "+beweryDetailsList.ElementAt(opt-1).Id.ToString());
                    Console.WriteLine("Name: "+beweryDetailsList.ElementAt(opt-1).Name);                    
                    createSelfName(loadData(computeURL(beweryDetailsList.ElementAt(opt - 1)._links.self.Href)));                    
                    Console.WriteLine("///////////////////");

                    /*Choose what to display next*/
                    Console.WriteLine("Press 1 for beers details,\nPress 0 for menu");
                    optString = Console.ReadLine();
                    optBeweryDetails = int.Parse(optString);
                    if (optBeweryDetails == 1)
                    {
                        /*Display beers*/
                        createBeers(loadData(computeURL(beweryDetailsList.ElementAt(opt - 1)._links.beers.Href)));                        
                    }
                }else if(opt == beweryDetailsList.Count()+1)
                {
                    /*Post method chosen*/
                    Post();
                }
            } while (opt != 0); /*Still not exit*/
        }

        private static string computeURL(string url)
        {
            string[] separator = { "/breweries" };
            string[] urltable = datcLink.Split(separator,StringSplitOptions.None);
            return urltable[0] + url;
        }

        private static string loadData(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync(url).Result;

            var data = response.Content.ReadAsStringAsync().Result;

            return data;
        }
        private static Self createSelfName(string data)
        {
            Self selfDataList = JsonConvert.DeserializeObject<Self>(data);
            Console.WriteLine("Self Name: "+selfDataList.Name);
            return selfDataList;
        }
        private static void createBeers(string data)
        {
            string optBeerDetails;
            int i=0;
            string[] separatorCase = { "_links" };
            string[] separatorCase2 = { "}}" };

            /*Get beers part*/
            string[] separator = { "embedded" };
            string[] dataExtracted = data.Split(separator, StringSplitOptions.None);

            if (dataExtracted.Length > 1)
            {
                /*Make a beer list*/
                string beweryData = createJsonString(dataExtracted[1]);
                List<BeerDetails> beerDetailsList = JsonConvert.DeserializeObject<List<BeerDetails>>(beweryData);


                /*Display inner menu*/
                do
                {
                    i = 0;
                    Console.Clear();
                    /*Display available beer atributes*/
                    foreach (BeerDetails bd in beerDetailsList)
                    {
                        i++;
                        Console.WriteLine("Beer No." + i);
                        Console.WriteLine("ID: " + bd.Id);
                        Console.WriteLine("Name: " + bd.Name);
                        Console.WriteLine("Brewery ID: " + bd.Breweryid);
                        Console.WriteLine("Brewery Name: " + bd.Breweryname);
                        Console.WriteLine("Style ID: " + bd.Styleid);
                        Console.WriteLine("Style Name: " + bd.Stylename);
                        Console.WriteLine("///////////////////");
                    }

                    Console.WriteLine("Choose one beer or Press 0 to return");
                    optBeerDetails = Console.ReadLine();

                    if (int.Parse(optBeerDetails) != 0 && int.Parse(optBeerDetails) < beerDetailsList.Count() + 1)
                    {
                        /*One beer chosen*/
                        string optSubMeniu;
                        Console.Clear();
                        Console.WriteLine("1.Style details");
                        Console.WriteLine("2.Back to Brewery");
                        optSubMeniu = Console.ReadLine();

                        /*Next possible choises*/
                        switch (optSubMeniu)
                        {
                            case "1":
                                /*Display self Details*/
                                Self selfObj = createSelfName(loadData(computeURL(beerDetailsList.ElementAt(int.Parse(optBeerDetails) - 1)._links.style.Href)));
                                Console.WriteLine("ID: " + selfObj.Id);
                                Console.WriteLine("Please return to brewery, press 1");
                                Console.ReadLine();
                                break;

                            case "2":
                                /*Go back to breweries*/
                                optBeerDetails = "0";
                                break;
                        }
                    }
                } while (int.Parse(optBeerDetails) != 0); /*Still beer details displaing*/
            }else
            {
                Console.WriteLine("Nothing to display! \n Press something to return");
                Console.ReadKey();
            }
        }
        private static string createJsonString(string dataExtracted)
        {
            /*Create an Json Object*/
            string[] separator2 = { "[" };
            string[] dataExtracted2 = dataExtracted.Split(separator2, StringSplitOptions.None);
            string[] separator3 = { "]" };
            string[] dataExtracted3 = dataExtracted2[1].Split(separator3, StringSplitOptions.None);
            return "[" + dataExtracted3[0] + "]";                        
        }

        public static void Post()
        {
            /*Create request*/
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://datc-rest.azurewebsites.net/beers");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            /*Send Message*/
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {               
                Self sobj = new Self();
                Console.WriteLine("Enter a name for a new beer");
                sobj.Name = Console.ReadLine();
                string json = JsonConvert.SerializeObject(sobj);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            /*Receive Response*/
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                if(responseText.CompareTo("") == 0)
                    Console.WriteLine("Done");
                else
                    Console.WriteLine("Failure");

                Console.WriteLine("Press something to continue");
                Console.ReadKey();                
            }
        }
    }
}
