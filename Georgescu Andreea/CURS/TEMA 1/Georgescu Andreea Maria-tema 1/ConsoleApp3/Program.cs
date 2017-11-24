using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3;
namespace ConsoleApp3
{
    class Program
    {
        
       
        static void Main(string[] args)
        {
            var data= Connect.ConnectWithAccept();
            var obj = JsonConvert.DeserializeObject<RootObj>(data);
            int choice;
            do
            {
                Console.WriteLine("1 Get Breweries \n2 Get Beers(Giving the name of the brewery \n3 Post Beer \n0 Exit");
                choice = Convert.ToInt32(Console.ReadLine()); 
                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Breweries: ");
                            Choice.getBreweries(obj);
                            break;
                        }
                    case 2:
                        {
                            Choice.getBeers(obj);
                            break;
                        }
                    case 3:
                        {
                            string name;
                            int id;
                            Console.WriteLine("Id for the new beer");
                            id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("The name of the new beer");
                            name = Console.ReadLine();
                            Choice.postBreweries(id, name, "http://datc-rest.azurewebsites.net/beers");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice");
                            break;
                        }
                }
            } while (choice != 0);
        }
    }
}
