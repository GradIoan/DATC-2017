using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        public static List<string> ListaPostMan = new List<string>();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return ListaPostMan.ToArray();
        }

        // GET api/values/5
        public string Get(int id)
        {
            if (id < ListaPostMan.Count)
                return ListaPostMan[id];
            else
                return "Index gresit";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            ListaPostMan.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            if (id < ListaPostMan.Count)
                ListaPostMan[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            if (id < ListaPostMan.Count)
                ListaPostMan.RemoveAt(id);
        }
    }
}
