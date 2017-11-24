using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class hal
    {
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

        public class Self2
        {
            public string href { get; set; }
        }

        public class Beers
        {
            public string href { get; set; }
        }

        public class Links2
        {
            public Self2 self { get; set; }
            public Beers beers { get; set; }
        }

        public class Brewery2
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Links2 _links { get; set; }
        }

        public class Embedded
        {
            public List<Brewery2> brewery { get; set; }
        }

        public class RootObject
        {
            public Links _links { get; set; }
            public Embedded _embedded { get; set; }
        }
    }
}
