using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

//HAL - Hypertext Application Language

namespace Hal.Client
{
	class Program
	{

    public static void Main(string[] args)
	{
            Int32 opt,flagCase3 = 0 ;
            string linkPornire = "http://datc-rest.azurewebsites.net/breweries";
            List<Brewery2B> berarii = new List<Brewery2B>();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
			var response = client.GetAsync(linkPornire).Result;

			var data = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<dynamic>(data);  // ce fel de obiect astept <dinamic> = orice obiect
            RootObjectB rootB = JsonConvert.DeserializeObject<RootObjectB>(data);  // RootObject -> clasa la care am mapat obiectul json
                    
            do
            {
                Console.WriteLine("\n1.Afisare tipuri Berarii");
                Console.WriteLine("2.Selectare tip Berarie + Afisare beri de acel tip");
                Console.WriteLine("3.Afisare Berarii + adaugare intr-o lista");
                Console.WriteLine("4.Selectare Berarie + Alege berea");
                Console.WriteLine("5.POST la o bere");
                Console.WriteLine("6.Iesire");
                Console.WriteLine("Optinuea dvs:");

                opt = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 1: //Afisare tipuri Berarii

                        foreach (Brewery2B berarie in rootB._embedded.brewery)
                        {
                            Console.WriteLine("Id:" + berarie.Id);
                            Console.WriteLine("Name(Style): " + berarie.Id + "." + berarie.Name);
                        }
                        break;

                    case 2: //Selectare tip Berarie + Afisare beri de acel tip

                        RootObject beers = null;
                        dynamic obiect;

                        foreach (Brewery2B berarie in rootB._embedded.brewery) //Afisarea tuturor tipurilor de berarii
                        {
                            Console.WriteLine("Name(Style): " + berarie.Id + "." + berarie.Name);
                        }

                        Console.WriteLine("Ce tip de berarie doriti sa vitizati?  Id = ");

                        int id = int.Parse(Console.ReadLine());

                         // Din linkul de pornire am devoie doar de "http://datc-rest.azurewebsites.net"(primele 34 caractere)iar in continuarea lui concatenez href-uri
                        string linkStilBerarie = linkPornire.Substring(0,34) + "/styles" + rootB._embedded.brewery[id-1]._links.beers.href.Substring(10);
                        response = client.GetAsync(linkStilBerarie).Result;
                        data = response.Content.ReadAsStringAsync().Result;

                        Console.WriteLine("Berile de tipul \"" + rootB._embedded.brewery[id - 1].Name + "\"\n");
                        try
                        {
                            beers = JsonConvert.DeserializeObject<RootObject>(data); 
                            foreach (Beer2 b in beers._embedded.beer)
                            {
                                Console.WriteLine(b.Id + ". " + b.Name);
                            }
                        }
                        catch(Exception e) // in caz ca am doar o bere in clasa Links eu am array de Beers!!!
                        {    
                            //solved with "dynamic" instead of List<Beer>
                        }
                  
                        break;

                    case 3:   //Afisare Berarii +Adaugare in Lista 

                        int i = 0;
                        response = client.GetAsync(linkPornire + "/" + (++i).ToString()).Result;
                        while (response.IsSuccessStatusCode == true) //cat timp am un raspuns ..parcurg Api-ul
                        {
                            data = response.Content.ReadAsStringAsync().Result;
                            Brewery2B brewery = JsonConvert.DeserializeObject<Brewery2B>(data);
                            berarii.Add(brewery);                                                  //le adaug in lista pentru a le putea folosi mai tarziu
                            Console.WriteLine("Name: " + brewery.Id + "." + brewery.Name);
                            response = client.GetAsync(linkPornire + "/" + (++i).ToString()).Result;
                        }
                        flagCase3 = 1;
                        break;

                    case 4: //Selectare Berarie + Afisare beri  + Alege berea

                        RootObject beri = null;
                        i = 0;
                        if (flagCase3==1) // se executa doar daca a fost executat inainte case 3 -> unde priemesc date despre 
                        {
                            Console.WriteLine("Ce berarie doriti sa vitizati?  Id = ");

                            int idBerarie = int.Parse(Console.ReadLine());

                            response = client.GetAsync(linkPornire + "/" + berarii[idBerarie - 1]._links.beers.href.Substring(10)).Result; //fac get pe o berarie(data de Id)
                            data = response.Content.ReadAsStringAsync().Result;
                            beri = JsonConvert.DeserializeObject<RootObject>(data);
                            foreach(Beer2 b in beri._embedded.beer) //afisarea berariilor din beraria cu Id-ul idBerarie
                            {
                                Console.WriteLine(++i +"->  " + b.Id + ". " + b.Name);
                            }

                            Console.WriteLine("Ce bere doriti?  Id bere(primul Id) = ");
                            int idBere = int.Parse(Console.ReadLine());

                            response = client.GetAsync(linkPornire.Substring(0,34) + beri._embedded.beer[i-1]._links.self.href).Result;
                            data = response.Content.ReadAsStringAsync().Result;
                            Beer2 b2 = JsonConvert.DeserializeObject<Beer2>(data);
                            Console.WriteLine("\nBerea dumneavoastra: ");
                            Console.WriteLine(b2); //ToString overrided -> afisare date despre berea dorita 
                        }
                        else
                        {
                            Console.WriteLine("Selectati optiunea 3 inainte!!!");
                        }
                        break;
                     case 5: // Adauga o bere noua POST

                        PostMethod();
                        break;
                }               

            } while (opt != 6);

			Console.ReadLine();
		}

        async public static void PostMethod()
        {
           
            Beer2 beer = new Beer2();
            Console.WriteLine("Id:");
            beer.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Name:");
            beer.Name = Console.ReadLine();
            Console.WriteLine("BreweryId:");
            beer.BreweryId = int.Parse(Console.ReadLine()); ;
            Console.WriteLine("BreweryName:");
            beer.BreweryName = Console.ReadLine();
            Console.WriteLine("StyleId:");
            beer.StyleId = int.Parse(Console.ReadLine()); ;
            Console.WriteLine("StyleName:");
            beer.StyleName = Console.ReadLine();

            var json = JsonConvert.SerializeObject(beer); //serializez datele despre bere intr-un obiect Json

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://datc-rest.azurewebsites.net/beers");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) // scrierea jsonu-lui
            {
                streamWriter.Write(json);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse(); 
        }
	}
}


