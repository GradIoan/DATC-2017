using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaDATC
{

    class MyList
    {
        public static List<Beers> Beers_List = new List<Beers>();
        public static List<Brewery> Breweries_List = new List<Brewery>();
        private static int i = 0;
        private static int j = 0;

        // FILL LISTS

        public static void Fill_Beers(JObject obj)
        {
            try
            {
                j = 0;
                while (obj["_embedded"]["beer"][j] != null)
                {
                    Beers New_Beer = new Beers(obj["_embedded"]["beer"][j]["StyleName"].ToString(), obj["_embedded"]["beer"][j]["Name"].ToString());
                    Beers_List.Add(New_Beer);
                    j++;
                }
            }
            catch (Exception)
            { }
        }
        public static void Fill_Breweries(JObject obj)
        {
            try
            {
                i = 0;
                while (obj["_embedded"]["brewery"][i]["Id"] != null)
                {
                    Brewery New_Brewery = new Brewery(Convert.ToInt16(obj["_embedded"]["brewery"][i]["Id"]), obj["_embedded"]["brewery"][i]["Name"].ToString(), obj["_embedded"]["brewery"][i]["_links"]["beers"]["href"].ToString());
                    Breweries_List.Add(New_Brewery);
                    i++;
                    Fill_Beers((JObject)JsonConvert.DeserializeObject(MyJObject.GET(StartUp.url + obj["_embedded"]["brewery"][i]["_links"]["beers"]["href"].ToString().Substring(10))));
                }
            }
            catch (Exception)
            {}
        }
    }
}
