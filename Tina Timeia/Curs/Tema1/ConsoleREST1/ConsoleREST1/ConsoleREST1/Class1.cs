using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleREST1
{
    public class tot
    {
        public class Beer
        {
            [DataMember(PropertyName = "Id")]
            public int Id { get; set; }
            [DataMember(PropertyName = "Name")]
            public string Name { get; set; }
            [DataMember(PropertyName = "_links")]
            public IList<object> _links { get; set; }
        }

        public class BeerList
        {


            [DataMember(PropertyName = "self")]
            // public IList<Beer> ResourceList { get; set; }
            public Link self { get; set; }
            [DataMember(PropertyName = "beers")]
            public Link Beer { get; set; }

            public static implicit operator List<object>(BeerList v)
            {
                throw new NotImplementedException();
            }
        }

        public class Link
        {
            [DataMember(PropertyName = "href")]
            public string href { get; set; }
        }

        public class embedded
        {
            [DataMember(PropertyName = "brewery")]
            public List<Beer> berarii { get; set; }

        }
    }
}
