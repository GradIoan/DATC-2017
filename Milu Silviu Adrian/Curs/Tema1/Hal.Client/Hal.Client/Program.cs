using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    
    class Program
	{
   
        static void Main(string[] args)
		{
			var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync("http://datc-rest.azurewebsites.net/breweries").Result;
          
            var data = response.Content.ReadAsStringAsync().Result;

            dynamic obj = JsonConvert.DeserializeObject<RootObject>(data);


            List<Brewery2> abc = new List<Brewery2>();
            abc = obj._embedded.brewery;
            foreach(Brewery2 x in abc)
            {
                Console.WriteLine(x.Id +"  "+ x.Name + "  "+x._links.beers.href);
                Console.WriteLine();
            }
 

            int i = -1;
            int opt;

      
            do
            {
                 opt = 0;
                Console.WriteLine("1. Intra pe o berarie");
                Console.WriteLine("2. Delete");
                Console.WriteLine("3. Post");
                Console.WriteLine("4. Exit");
                Console.WriteLine("introduceti optiunea dvs :");
                try
                {
                    opt = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("introdu ceva valid");
                }
                switch (opt)
                {

                    default:
                        {
                          //  Console.WriteLine(obj.ResourceList[i]);
                           
                            break;
                        }
                    case 1:
                        {
                            // la style direct , trebuie back si page 
                                Console.WriteLine("Numarul berariei ?");
                                int aux = Convert.ToInt32(Console.ReadLine());
                                string s1 = "http://datc-rest.azurewebsites.net";
                                string s2 = abc[aux-1]._links.beers.href;
                                string sfin = s1 + s2;
                                    Console.WriteLine(sfin);

                                var clientbeer = new HttpClient();
                                clientbeer.DefaultRequestHeaders.Add("Accept", "application/hal+json");
                                var responsebeer = client.GetAsync(sfin).Result;

                                var databeer = responsebeer.Content.ReadAsStringAsync().Result;
                                   //    Console.WriteLine(data);
                                dynamic objbeer = JsonConvert.DeserializeObject<RootObjectBeer>(databeer);
                                dynamic objbeerdebug = JsonConvert.DeserializeObject(databeer);
                                Console.WriteLine(objbeerdebug);

                            try
                            {
                                List<Beer2Beer> abcbeer = new List<Beer2Beer>();
                                abcbeer = objbeer._embedded.beer;

                                List<string> styles = new List<string>();
                                List<string> self = new List<string>();
                                List<string> brew = new List<string>();

                                int count = 0;
                                foreach (Beer2Beer x in abcbeer)
                                {
                                    Console.WriteLine(count.ToString()+"."+x.Name + "\n");
                                    styles.Add(x._links.style.href.ToString());
                                    self.Add(x._links.self.href.ToString());
                                    brew.Add(x._links.brewery.href.ToString());
                                    count++;
                                }
                                int nr;
                                Console.WriteLine("Pe care bere ?");
                                nr = Convert.ToInt32(Console.ReadLine());


                                int menuaux;
                                Console.WriteLine("1. Intra pe bere efectiv\n 2. Intra pe stilul  berii \n3. Mergi pe link-ul berariei de care apartine berea");
                                menuaux = Convert.ToInt32(Console.ReadLine());

                                if (menuaux == 1)
                                {
                                    string selffin = s1 + self.ElementAt(nr);
                                    Console.WriteLine(selffin);
                                    var clientself = new HttpClient();
                                    clientself.DefaultRequestHeaders.Add("Accept", "application/hal+json");
                                    var responseself = client.GetAsync(selffin).Result;
                                    var dataself = responseself.Content.ReadAsStringAsync().Result;
                                    Console.WriteLine(dataself);
                                    dynamic objself = JsonConvert.DeserializeObject(dataself);
                                    Console.WriteLine(objself);

                                    Console.WriteLine("Intra si pe review ?(1/0) *daca este* ");
                                    int a = 0;
                                    a = Convert.ToInt32(Console.ReadLine());
                                    if (a == 1)
                                    {
                          //              Console.WriteLine(objself._links.review.href);
                                        string linkrew = s1 + objself._links.review.href;
                                        Console.WriteLine(linkrew);
                                        var clientrew = new HttpClient();
                                        clientself.DefaultRequestHeaders.Add("Accept", "application/hal+json");
                                        var responserew = client.GetAsync(linkrew).Result;
                                        var datarew = responserew.Content.ReadAsStringAsync().Result;
                              //          Console.WriteLine(datarew);
                                        dynamic objrew = JsonConvert.DeserializeObject(datarew);
                                        Console.WriteLine(objrew);
                                    }
                                }
                                if (menuaux == 2)
                                {
                                    string stylefin = s1 + styles.ElementAt(nr);
                                    Console.WriteLine(stylefin);
                                    var clientstyle = new HttpClient();
                                    clientstyle.DefaultRequestHeaders.Add("Accept", "application/hal+json");
                                    var responsestyle = client.GetAsync(stylefin).Result;
                                    var datastyle = responsestyle.Content.ReadAsStringAsync().Result;
                                    Console.WriteLine(datastyle);
                                    dynamic objstyle = JsonConvert.DeserializeObject(datastyle);
                                    Console.WriteLine(objstyle);
                                }
                              
                                if(menuaux == 3)
                                {
                                    string brewfin = s1 + brew.ElementAt(nr);
                                    Console.WriteLine(brewfin);
                                    var clientbrew = new HttpClient();
                                    clientbrew.DefaultRequestHeaders.Add("Accept", "application/hal+json");
                                    var responsebrew = client.GetAsync(brewfin).Result;
                                    var databrew = responsebrew.Content.ReadAsStringAsync().Result;
                                    Console.WriteLine(databrew);
                                    dynamic objbrew = JsonConvert.DeserializeObject(databrew);
                                    Console.WriteLine(objbrew);
                                  
                                }
                           

                            }
                            catch
                            {
                                Console.WriteLine("Something went wrong :(, maybe nothing there ?");
                            }
                            break;
                        }
                    case 2:
                        {
                            var clientdel = new HttpClient();
                            clientdel.BaseAddress = new Uri("http://datc-rest.azurewebsites.net/beers/");

                            Console.WriteLine("Ce bere doriti sa stergeti ? Dati numarul :");
                            string delstring = Console.ReadLine();

                            var delresponse = clientdel.DeleteAsync(delstring).Result;
                            Console.WriteLine(delresponse);
                            break;
                        }
                    case 3:
                        {

                            Console.WriteLine("Numele berii ?");
                            string numeb = Console.ReadLine();

                            Beer2Beer bx = new Beer2Beer();
                  
                            bx.Id = 200;
                            bx.Name = numeb;
                            bx.BreweryId = 5;
                            bx.BreweryName = "English Pale Ale";
                            bx.StyleId = 1;
                            bx.StyleName = "rambo";

                            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(bx);

                            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                            var result = client.PostAsync("http://datc-rest.azurewebsites.net/beers", content).Result;
                            Console.WriteLine(result);
                            break;
                        }     

                }
            } while (opt != 4);

            Console.ReadLine();
          
        }
    }
}
