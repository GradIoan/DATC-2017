using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1
{
    public class Interfata
    {
        public static void Meniu_Berarii(int opt, List<Berarie> lst)
        {

            int opt_OK = 0;
            string href = null;
            JObject obj = null;

            foreach (var elem in lst)
            {
                if(opt == elem.ID)
                {
                    opt_OK = 1;
                    href = elem.Link;
                }
            }   
            if(opt_OK == 1)
            {

                href = href.Substring(10); //takes the needed resource from href
                obj = DataHandler.Get_Json(DataHandler.URL + href);
                List<Bere> lista_beri = new List<Bere>();
                lista_beri = DataHandler.GetBeersData(obj);
                Interfata.Afisare_Beri(lista_beri, opt);
                opt_OK = 0;
            }
            else
            {
                Console.WriteLine("Optiunea introdusa este gresita.\nVeti fi directionati catre meniul principal.");
                Console.ReadLine();
            }
            Console.Clear();
        }

        public static void Afisare_Berarii(List<Berarie> lst_berarii)
        {
            foreach (var element in lst_berarii)
            {
                Console.WriteLine(element.ID.ToString() + ") " + element.Nume.ToString());
            }
            Console.WriteLine("\n");
        }

        public static void Afisare_Beri(List<Bere> lst_beri, int opt)
        {
            foreach (var elem in lst_beri)
            {
                if (elem.Id_Berarie == opt)
                {
                    Console.WriteLine("ID: " + elem.Id.ToString() + "\n"
                                + "Nume: " + elem.Nume.ToString() + " \n"
                                + "Id Berarie: " + elem.Id_Berarie.ToString() + " \n"
                                + "Nume Berarie: " + elem.NumeBerarie.ToString() + " \n"
                                + "Style ID: " + elem.StyleID.ToString() + " \n"
                                + "Style Name: " + elem.StyleName.ToString() + " \n");
                }
            }
            Console.WriteLine("\nPress any key to resume.");
            Console.ReadLine();
        }
    }
}
