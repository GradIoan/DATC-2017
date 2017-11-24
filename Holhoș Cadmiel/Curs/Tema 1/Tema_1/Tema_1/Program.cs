using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_1
{
    class Program
    {
        

        static void Main(string[] args)
        {
            int optiune_principala = 1, optiune_berarii = 1, optiune_beri = 1, opt_OK = 0 ;

            JObject obj = null;
            

            obj = DataHandler.Get_Json(DataHandler.URL);

            while(0 != optiune_principala)
            {
                Console.Clear();
                Console.WriteLine("Alege o optiune din meniul de mai jos.\n0 - Iesire.\n1 - Vizualizare Berarii.\n2 - Adauga o bere.\n");
                try
                {
                    optiune_principala = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    optiune_principala = 0;
                }
                Console.Clear();
                switch (optiune_principala)
                {
                    case 1://show breweries
                        List<Berarie> lista_berarii = new List<Berarie>();
                        lista_berarii = DataHandler.GetBreweriesData(obj);
                        Interfata.Afisare_Berarii(lista_berarii);

                        
                        Console.WriteLine("Introduceti ID-ul berariei pentru vizualizare beri, sau orice alta valoare pentru iesire.");
                        try
                        {
                            optiune_berarii = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            optiune_berarii = 0;
                        }
                        Console.Clear();
                        Interfata.Meniu_Berarii(optiune_berarii, lista_berarii);

                        break;
                    case 2:
                        DataHandler.Adauga_Bere();
                        break;
                    default:
                        break; 
                }


            }


        }
    }
}
