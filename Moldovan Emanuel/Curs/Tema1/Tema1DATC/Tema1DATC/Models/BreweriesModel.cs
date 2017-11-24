using System.Collections.Generic;
using System.Web.Helpers;

namespace Tema1DATC.Models
{
    class BreweriesModel
    {
        public Links _links { get; set; }
        public BreweriesModel()
        {
         
        }
        public BreweriesModel(string json)
        {
            this._links = Json.Decode<BreweriesModel>(json)._links;
        }
        public class Self
        {
            public string href { get; set; }
        }

        public class Brewery
        {
            public string href { get; set; }
        }

        public class Links
        {
            public Self self { get; set; }
            public List<Brewery> brewery { get; set; }
        }
       
    }
}
