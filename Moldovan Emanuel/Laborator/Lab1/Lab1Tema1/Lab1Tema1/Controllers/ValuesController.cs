using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lab1Tema1.Controllers
{
    public class ValuesController : ApiController
    {
        static List<ListaLab1> lista = new List<ListaLab1>();
        // GET api/values
        public IEnumerable<ListaLab1> Get()
        {
            return lista;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return lista.First(w => w.Key == id).Valuare;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            ListaLab1 obiect = new ListaLab1()
            {
                Key = lista.Count(),
                Valuare = value
            };

            lista.Add(obiect);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            var update = lista.First(w => w.Key == id);

            if(update != null)
            {
                update.Key = id;
                update.Valuare = value; 
            }
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            var delete = lista.First(w => w.Key == id);

            if (delete != null)
            {
                lista.Remove(delete);
            }
        }
    }
}
