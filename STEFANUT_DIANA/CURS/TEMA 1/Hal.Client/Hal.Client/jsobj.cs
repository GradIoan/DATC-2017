using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{

    public class ResourceList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<object> _links { get; set; }
    }

    public class RootObject
    {
        public List<ResourceList> ResourceList { get; set; }
        public List<object> _links { get; set; }
    }
}
