using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TemaDATC
{
    class StartUp
    {
        //URL
        public static string url = "http://datc-rest.azurewebsites.net/breweries";
        private static string url_response;
        private static object converted_response;
        private ushort option;
        private int new_id;
        private string new_name;
        static void Main(string[] args)
        {
            StartUp startup = new StartUp();
                        
            url_response = MyJObject.GET(url);
            converted_response = JsonConvert.DeserializeObject(url_response);
            JObject json_object = (JObject)converted_response;
            MyList.Fill_Breweries(json_object);

            incorrect_input:
            Console.WriteLine("Choose an option:");
            ShowData.Show_menu();
            Console.Write(Environment.NewLine + "Option: ");
            try
            {
                startup.option = Convert.ToUInt16(Console.ReadLine());
            }
            catch(Exception)
            {
                Console.WriteLine("INCORRECT INPUT" + Environment.NewLine);
                goto incorrect_input;
            }

            while(startup.option!=0)
            {

                switch (startup.option)
                {
                    case 1:
                        ShowData.Show_Breweries();
                        break;
                    case 2:
                        ShowData.Show_Beers();
                        break;
                    case 3:
                        Console.WriteLine("Brewery name or a part of it:");
                        ShowData.Show_Beers_by_Breweries(Console.ReadLine());
                        break;
                    case 4:
                        Console.WriteLine("Dati ID: ");
                        startup.new_id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Dati Numele: ");
                        startup.new_name = Console.ReadLine();
                        MyJObject.POST(url, startup.new_id, startup.new_name);
                        break;
                    default:
                        break;
                }

                Console.WriteLine("Choose an option:");
                ShowData.Show_menu();
                Console.Write(Environment.NewLine + "Option: ");
                try
                {
                    startup.option = Convert.ToUInt16(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("INCORRECT INPUT" + Environment.NewLine);
                    goto incorrect_input;
                }

            } 
        }
    }
}
