using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Tema1DATC
{
    class BreweryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Links _links { get; set; }

        public BreweryModel()
        {

        }
        public BreweryModel(string json)
        {
            if(json != null)
            {
                var obj = Json.Decode<BreweryModel>(json);

                this.Id = obj.Id;
                this.Name = obj.Name;
                this._links = obj._links;
            }
            else
            {

            }
            
        }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Beers
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Beers beers { get; set; }
    }

}
