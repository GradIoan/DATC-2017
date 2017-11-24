using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ExempluController : ApiController
    {

        private static Dictionary<int, string> exemplu = new Dictionary<int, string>();


        // GET: api/Exemplu
        public IHttpActionResult Get()
        {
            exemplu.Add(1, "Exemplu LAB Datc");
            return Ok(exemplu);
        }

        // GET: api/Exemplu/5
        public IHttpActionResult Get(int id)
        {
            var x = exemplu.FirstOrDefault(t => t.Key.Equals(id));
            if (x.Equals(default(KeyValuePair<int, string>)))
            {
                return NotFound();
            }
            return Ok(x);
        }

        // POST: api/Exemplu
        public IHttpActionResult Post([FromBody]KeyValuePair<int, string> valoare)
        {
            exemplu.Add(valoare.Key, valoare.Value);
            return Created(valoare.Key.ToString(), valoare);
        }

        // PUT: api/Exemplu/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Exemplu/5
        public void Delete(int id)
        {
        }
    }
}
