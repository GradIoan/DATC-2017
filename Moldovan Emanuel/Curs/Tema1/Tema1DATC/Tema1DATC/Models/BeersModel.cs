using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Tema1DATC.Models
{
    class BeersModel
    {
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public Links _links { get; set; }

        public BeersModel()
        {

        }
        public BeersModel(string json)
        {
            if (json != null)
            {
                var obj = Json.Decode<BeersModel>(json);

                this.TotalResults = obj.TotalResults;
                this.TotalPages = obj.TotalPages;
                this.Page = obj.Page;
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

    public class Page
    {
        public string href { get; set; }
        public bool templated { get; set; }
    }

    public class Beer
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public List<Page> page { get; set; }
        public List<Beer> beer { get; set; }
        public Brewery brewery { get; set; }
    }
}
