using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hal.Client
{
    class ClassObjects_Breweries_old
    {
        public class Rootobject
        {
            public _Links _links { get; set; }
            public _Embedded _embedded { get; set; }
        }

        public class _Links
        {
            public Self self { get; set; }
            public Brewery[] brewery { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Brewery
        {
            public string href { get; set; }
        }

        public class _Embedded
        {
            public Brewery1[] brewery { get; set; }
        }

        public class Brewery1
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public _Links1 _links { get; set; }
        }

        public class _Links1
        {
            public Self1 self { get; set; }
            public Beers beers { get; set; }
        }

        public class Self1
        {
            public string href { get; set; }
        }

        public class Beers
        {
            public string href { get; set; }
        }

    }
}