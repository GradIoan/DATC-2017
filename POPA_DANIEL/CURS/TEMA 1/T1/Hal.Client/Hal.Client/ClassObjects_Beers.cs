using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class ClassObjects_Beers
    {
        public class ResourceList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<object> _links { get; set; }
        }

        public class RootObject
        {
            public int TotalResults { get; set; }
            public int TotalPages { get; set; }
            public int Page { get; set; }
            public List<ResourceList> ResourceList { get; set; }
            public List<object> _links { get; set; }
        }
    }
}
