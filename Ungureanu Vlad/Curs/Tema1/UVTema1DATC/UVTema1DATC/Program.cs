using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace UVTema1DATC
{

    class Program
    {
        public static string Home = "http://datc-rest.azurewebsites.net/";
        public static List<Breweries> FabriciDeBere = new List<Breweries>();
        public static List<Beers> ListaBeri = new List<Beers>();
        public static HttpClient client = new HttpClient();

        public static string ClearString(string data)
        {
            data = data.Replace("{", "");
            data = data.Replace("}", "");
            data = data.Replace("\"", "");
            data = data.Replace(",", "");
            data = data.Replace("]", "");
            data = data.Replace("[", "");
            return data;
        }
        public static void AfisareMeniuPrincipal()
        {
            FabriciDeBere.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/hal+json");
            var response = client.GetAsync(Home + "breweries/").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            data = ClearString(data);
            string[] dataSplit = data.Split(new string[] { "_embedded" }, StringSplitOptions.RemoveEmptyEntries);
            dataSplit = dataSplit[1].Split(new string[] { "Id:" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                string[] dateFabrica = dataSplit[i + 1].Split(new string[] { "Name:" }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(dateFabrica[0]);
                dateFabrica = dateFabrica[1].Split(new string[] { "_links:" }, StringSplitOptions.RemoveEmptyEntries);
                string name = dateFabrica[0];
                dateFabrica = dateFabrica[1].Split(new string[] { "href:" }, StringSplitOptions.RemoveEmptyEntries);
                string linkToBrewerie = dateFabrica[1].Replace("beers:", "");
                string linkToBeers = dateFabrica[2];
                FabriciDeBere.Add(new Breweries(id, name, linkToBeers, linkToBrewerie));
            }
            Console.Clear();
            Console.WriteLine("-----Meniu tipuri de bere-----");
            for (int i = 0; i < FabriciDeBere.Count; i++)
            {
                Console.WriteLine("Id:" + FabriciDeBere[i].Id);
                Console.WriteLine("Tip de bere:" + FabriciDeBere[i].Name);
                Console.WriteLine("--------------------------");
            }
        }

        public static string AfisareDetaliiBerarie(string optiune)
        {
            int optiune2 = 0;
            string linkToBeers = "";
            var response = client.GetAsync(Home + optiune).Result;
            string data = response.Content.ReadAsStringAsync().Result;
            data = ClearString(data);
            if (data.Contains("Message") && data.Contains("An error has occurred"))
            {
                Console.Clear();
                Console.WriteLine("------Informatii Berarie-----");
                Console.WriteLine("Nu exista informatii despre berarie, apasati orice tasta pentru a revenii la meniul principal");
                Console.ReadKey();
            }
            else
            {
                string[] dateFabrica = data.Split(new string[] { "Id:" }, StringSplitOptions.RemoveEmptyEntries);
                dateFabrica = dateFabrica[0].Split(new string[] { "Name:" }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(dateFabrica[0]);
                dateFabrica = dateFabrica[1].Split(new string[] { "_links:" }, StringSplitOptions.RemoveEmptyEntries);
                string name = dateFabrica[0];
                dateFabrica = dateFabrica[1].Split(new string[] { "href:" }, StringSplitOptions.RemoveEmptyEntries);
                linkToBeers = dateFabrica[2];
                Console.Clear();
                Console.WriteLine("------Informatii Berarie-----");
                Console.WriteLine("Id:" + id.ToString());
                Console.WriteLine("Nume:" + name);
                Console.WriteLine("0. Meniul principal");
                Console.WriteLine("1. Vizualizare beri");
                Console.Write("Optiune:");
                int.TryParse(Console.ReadLine(), out optiune2);
            }
            return optiune2.ToString() + "@" + linkToBeers;
        }

        public static string AfisareDetaliiBere(string info)
        {
            ListaBeri.Clear();
            string linkToBeer = "", linkToStil = "", linkToBrewerie = "";
            int optiune = 0, optid = 0; ;
            var response = client.GetAsync(Home + info).Result;
            string data = response.Content.ReadAsStringAsync().Result;
            data = data.Replace("BreweryId", "Brewery");
            data = data.Replace("StyleId", "Style");
            data = ClearString(data);
            string[] dataSplit = data.Split(new string[] { "_embedded" }, StringSplitOptions.RemoveEmptyEntries);
            dataSplit = dataSplit[1].Split(new string[] { "Id:" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                string[] dateBere = dataSplit[i + 1].Split(new string[] { "Name:" }, StringSplitOptions.RemoveEmptyEntries);
                int id = int.Parse(dateBere[0]);
                string[] dateBere1 = dateBere[1].Split(new string[] { "Brewery:" }, StringSplitOptions.RemoveEmptyEntries);
                string nume = dateBere1[0];
                dateBere1[1] = dateBere1[1].Replace("Brewery", "");
                int idBerarie = int.Parse(dateBere1[1]);
                string[] dateBere2 = dateBere[2].Split(new string[] { "Style:" }, StringSplitOptions.RemoveEmptyEntries);
                string numeBerarie = dateBere2[0];
                dateBere2[1] = dateBere2[1].Replace("Style", "");
                int idStil = int.Parse(dateBere2[1]);
                dateBere = dateBere[3].Split(new string[] { "_links:" }, StringSplitOptions.RemoveEmptyEntries);
                string numeStil = dateBere[0];
                dateBere = dateBere[1].Split(new string[] { "href:" }, StringSplitOptions.RemoveEmptyEntries);
                linkToBeer = dateBere[1].Replace("style:", " ");
                linkToStil = dateBere[2].Replace("brewery:", "");
                linkToBrewerie = dateBere[3];
                ListaBeri.Add(new Beers(id, nume, idBerarie, numeBerarie, idStil, numeStil, linkToBeer, linkToBrewerie, linkToStil, ""));
            }
            Console.Clear();
            Console.WriteLine("-----Informatii bere-----");
            for (int i = 0; i < ListaBeri.Count; i++)
            {
                Console.WriteLine("Id bere:" + ListaBeri[i].Id);
                Console.WriteLine("Nume bere:" + ListaBeri[i].Name);
                Console.WriteLine("Id berarie:" + ListaBeri[i].IdBerarie);
                Console.WriteLine("Nume berarie:" + ListaBeri[i].NameBerarie);
                Console.WriteLine("Id stil:" + ListaBeri[i].IdStil);
                Console.WriteLine("Nume stil:" + ListaBeri[i].NameStil);
                Console.WriteLine("--------------------------");
            }
            Console.WriteLine("0.Meniu principal");
            Console.WriteLine("1.Mai multe informatii bere dupa id");
            Console.WriteLine("2.Inapoi la berarie");
            Console.Write("Optiune:");
            int.TryParse(Console.ReadLine(), out optiune);
            int k = 0;
            if (optiune == 1)
            {
                Console.Write("Id:");
                if (int.TryParse(Console.ReadLine(), out optid))
                {
                    for (int j = 0; j < ListaBeri.Count; j++)
                    {
                        if (ListaBeri[j].Id == optid)
                            k = j;
                    }
                }
            }
            return optiune.ToString() + "@" + k.ToString();
        }

        public static string AfisareMaiMulteDetaliiBere(int index)
        {
            int optiune = 0;
            var response = client.GetAsync(Home + ListaBeri[index].LinkToBeer).Result;
            string data = response.Content.ReadAsStringAsync().Result;
            data = ClearString(data);
            string[] dataSplit = data.Split(new string[] { "review:href:" }, StringSplitOptions.RemoveEmptyEntries);
            if (dataSplit.Length == 2)
                ListaBeri[index].LinkToReview = dataSplit[1];
            Console.Clear();
            Console.WriteLine("-----Mai multe informatii bere-----");
            Console.WriteLine("Id bere:" + ListaBeri[index].Id);
            Console.WriteLine("Nume bere:" + ListaBeri[index].Name);
            Console.WriteLine("Id berarie:" + ListaBeri[index].IdBerarie);
            Console.WriteLine("Nume berarie:" + ListaBeri[index].NameBerarie);
            Console.WriteLine("Id stil:" + ListaBeri[index].IdStil);
            Console.WriteLine("Nume stil:" + ListaBeri[index].NameStil);
            Console.WriteLine("--------------------------");
            Console.WriteLine("0.Meniu principal");
            Console.WriteLine("1.Inapoi la berarie");
            Console.WriteLine("2.Informatii stil");
            Console.WriteLine("3.Informatii review");
            Console.Write("Optiune:");
            int.TryParse(Console.ReadLine(), out optiune);
            return optiune.ToString();
        }
        static void Main(string[] args)
        {
            int optiune = 1, optiune2 = 0;
            string infoBeri;
            while (optiune != 0)
            {
                if (optiune2 == 0)
                {
                    AfisareMeniuPrincipal();
                    Console.WriteLine("Alege un tip dupa id pentru mai multe detalii");
                    Console.WriteLine("Alege -1 pentru introducerea unei beri");
                    Console.WriteLine("Alege 0 sau orice litera pentru iesire");
                    Console.Write("Optiune:");
                    int.TryParse(Console.ReadLine(), out optiune);
                }
                if (optiune == -1)
                {
                    //POST Beer
                    int idBeree = 0;
                    string numeBeree = "";
                    Console.Clear();
                    Console.WriteLine("-----Adauga Bere-----");
                    Console.Write("IdBere:");
                    int.TryParse(Console.ReadLine(), out idBeree);
                    Console.Write("NumeBere:");
                    numeBeree = Console.ReadLine();
                    PostBeer newBeer = new PostBeer(idBeree, numeBeree);
                    StringContent continut = new StringContent(JsonConvert.SerializeObject(newBeer));
                    var ClientHTTP = new HttpClient();
                    ClientHTTP.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    continut.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    var raspuns = ClientHTTP.PostAsync("http://datc-rest.azurewebsites.net/beers", continut).Result;
                    Console.WriteLine(raspuns);
                    Console.WriteLine("Apasati orice tasta pentru a revenii la meniul principal");
                    Console.ReadKey();
                }
                if (optiune > 0 && optiune <= FabriciDeBere.Count)
                {
                    //Detalii berarie
                    infoBeri = AfisareDetaliiBerarie(FabriciDeBere[optiune - 1].LinkToBrewerie);
                    string[] infosplit = infoBeri.Split('@');
                    if (infosplit[0].Equals("0"))
                    {
                        //meniu principal
                        optiune2 = 0;
                    }
                    if (infosplit[0].Equals("1"))
                    {
                        //detalii beri
                        infoBeri = AfisareDetaliiBere(infosplit[1]);
                        string[] infoDetalii = infoBeri.Split('@');
                        if (infoDetalii[0].Equals("0"))
                        {
                            //meniu principal
                            optiune2 = 0;
                        }
                        if (infoDetalii[0].Equals("1"))
                        {
                            //detalii bere
                            infoBeri = AfisareMaiMulteDetaliiBere(int.Parse(infoDetalii[1]));
                            string[] moreInfo = infoBeri.Split('@');
                            if (moreInfo[0].Equals("0"))
                            {
                                //meniu principal
                                optiune2 = 0;
                            }
                            if (moreInfo[0].Equals("1"))
                            {
                                //berarie
                                optiune2 = 1;
                            }
                            if (moreInfo[0].Equals("2"))
                            {
                                //stil
                                var response = client.GetAsync(Home + ListaBeri[int.Parse(infoDetalii[1])].LinkToStyle).Result;
                                string data = response.Content.ReadAsStringAsync().Result;
                                data = response.Content.ReadAsStringAsync().Result;
                                data = ClearString(data);
                                data = data.Replace("Id:", "");
                                string[] info = data.Split(new string[] { "Name:" }, StringSplitOptions.RemoveEmptyEntries);
                                int id = int.Parse(info[0]);
                                info = info[1].Split(new string[] { "_links:" }, StringSplitOptions.RemoveEmptyEntries);
                                string name = info[0];
                                info = info[1].Split(new string[] { "beers:href:" }, StringSplitOptions.RemoveEmptyEntries);
                                string linkStil = info[1];
                                Console.Clear();
                                Console.WriteLine("-----Informatii stil-----");
                                Console.WriteLine("Id:" + id.ToString());
                                Console.WriteLine("Nume:" + name);
                                Console.WriteLine("--------------------------");
                                Console.WriteLine("0. Inapoi la meniu");
                                Console.WriteLine("1. Vezi berea");
                                Console.Write("Optiune:");
                                int optiune3 = 0;
                                int.TryParse(Console.ReadLine(), out optiune3);
                                if (optiune3 == 1)
                                {
                                    //detalii bere
                                    var response1 = client.GetAsync(Home + linkStil).Result;
                                    string data1 = response1.Content.ReadAsStringAsync().Result;
                                    data1 = data1.Replace("BreweryId", "Brewery");
                                    data1 = data1.Replace("StyleId", "Style");
                                    data1 = ClearString(data1);
                                    string[] dataSplit = data1.Split(new string[] { "_embedded:" }, StringSplitOptions.RemoveEmptyEntries);
                                    dataSplit = dataSplit[1].Split(new string[] { "Id:" }, StringSplitOptions.RemoveEmptyEntries);
                                    string[] dateBere = dataSplit[1].Split(new string[] { "Name:" }, StringSplitOptions.RemoveEmptyEntries);
                                    int id1 = int.Parse(dateBere[0]);
                                    string[] dateBere1 = dateBere[1].Split(new string[] { "Brewery:" }, StringSplitOptions.RemoveEmptyEntries);
                                    string nume = dateBere1[0];
                                    dateBere1[1] = dateBere1[1].Replace("Brewery", "");
                                    int idBerarie = int.Parse(dateBere1[1]);
                                    string[] dateBere2 = dateBere[2].Split(new string[] { "Style:" }, StringSplitOptions.RemoveEmptyEntries);
                                    string numeBerarie = dateBere2[0];
                                    dateBere2[1] = dateBere2[1].Replace("Style", "");
                                    int idStil = int.Parse(dateBere2[1]);
                                    dateBere = dateBere[3].Split(new string[] { "_links:" }, StringSplitOptions.RemoveEmptyEntries);
                                    string numeStil = dateBere[0];
                                    Console.Clear();
                                    Console.WriteLine("-----Informatii bere din stil-----");
                                    Console.WriteLine("Id bere:" + id1.ToString());
                                    Console.WriteLine("Nume bere:" + nume);
                                    Console.WriteLine("Id berarie:" + idBerarie);
                                    Console.WriteLine("Nume berarie:" + numeBerarie);
                                    Console.WriteLine("Id stil:" + idStil);
                                    Console.WriteLine("Nume stil:" + numeStil);
                                    Console.WriteLine("--------------------------");
                                    Console.WriteLine("Apasati orice tasta pentru a reveni la meniu");
                                    Console.ReadKey();
                                }

                            }
                            if (moreInfo[0].Equals("3"))
                            {
                                //review
                                if (ListaBeri[int.Parse(infoDetalii[1])].LinkToReview.Equals(""))
                                {
                                    Console.Clear();
                                    Console.WriteLine("Ne exista review pentru aceasta bere, apasati orice tasta pentru a revenii la meniul principal");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    if (ListaBeri[int.Parse(infoDetalii[1])].LinkToReview.Contains("href:"))
                                    {
                                        string[] linkuri = ListaBeri[int.Parse(infoDetalii[1])].LinkToReview.Split(new string[] { "href:" }, StringSplitOptions.RemoveEmptyEntries);
                                        for (int i = 0; i < linkuri.Length; i++)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Review" + i.ToString());
                                            var response = client.GetAsync(Home + linkuri[i]).Result;
                                            string data = response.Content.ReadAsStringAsync().Result;
                                            data = response.Content.ReadAsStringAsync().Result;
                                            if (data.Contains("removed") || data.Contains("changed") || data.Contains("unavailable"))
                                            {
                                                Console.WriteLine("Ne exista review pentru aceasta bere, apasati orice tasta pentru a continua");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Console.WriteLine(data);
                                                Console.WriteLine("Apasati orice tasta pentru a continua");
                                                Console.ReadKey();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var response = client.GetAsync(Home + ListaBeri[int.Parse(infoDetalii[1])].LinkToReview).Result;
                                        string data = response.Content.ReadAsStringAsync().Result;
                                        data = response.Content.ReadAsStringAsync().Result;
                                        if (data.Contains("removed") || data.Contains("changed") || data.Contains("unavailable"))
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Ne exista review pentru aceasta bere, apasati orice tasta pentru a revenii la meniul");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine(data);
                                            Console.WriteLine("Apasati orice tasta pentru a revenii la meniul principal");
                                            Console.ReadKey();
                                        }
                                    }
                                }
                            }
                        }
                        if (infoDetalii[0].Equals("2"))
                        {
                            //berarie
                            optiune2 = 1;
                        }
                    }
                }
            }
        }
    }
}
