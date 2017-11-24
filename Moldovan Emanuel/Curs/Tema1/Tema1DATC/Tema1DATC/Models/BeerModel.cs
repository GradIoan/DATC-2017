using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Tema1DATC.Models
{
    class BeerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BreweryId { get; set; }
        public string BreweryName { get; set; }
        public int StyleId { get; set; }
        public string StyleName { get; set; }
        public Links _links { get; set; }

        public BeerModel()
        {

        }
        public BeerModel(string json)
        {
            if (json != null)
            {
                var obj = Json.Decode<BeerModel>(json);

                this.Id = obj.Id;
                this.Name = obj.Name;
                this.BreweryId = obj.BreweryId;
                this.BreweryName = obj.BreweryName;
                this.StyleId = obj.StyleId;
                this.StyleName = obj.StyleName;
                this._links = obj._links;
            }
            else
            {

            }

        }
    } 

    public class Style
    {
        public string href { get; set; }
    }

    public class Brewery
    {
        public string href { get; set; }
    }  
}
