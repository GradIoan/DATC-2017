using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hal.Client
{
    //class Bere
    //{
    //}
    public class Self
    {
        public string href { get; set; }

    }

    public class Next
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
        public Next next { get; set; }
        public List<Page> page { get; set; }
        public List<Beer> beer { get; set; }

    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Links2
    {
        public Self2 self { get; set; }
    }

    public class Beer2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Links2 _links { get; set; }
    }

    //public class Embedded
    //{
    //    public List<Beer2> beer { get; set; }
    //}

    public class RootObject
    {
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public Links _links { get; set; }
        //public Embedded _embedded { get; set; }

        public static explicit operator RootObject(JObject v)
        {
            throw new NotImplementedException();
        }
    }
}
