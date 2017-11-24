using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class Lab1DATCController : ApiController
    {

        private static Dictionary<int, string> dataBaseLab1 = new Dictionary<int, string>();

        // GET: api/Lab1DATC
        public IHttpActionResult Get()
        {
            dataBaseLab1.Add(1, "Andrei Miklos Lab 1");
            return Ok(dataBaseLab1);
        }

        // GET: api/Lab1DATC/5
        public IHttpActionResult Get(int id)
        {
            var dbElement = dataBaseLab1.FirstOrDefault(t => t.Key.Equals(id));
            if (dbElement.Equals(default(KeyValuePair<int, string>)))
            {
                return NotFound();
            }
            return Ok(dbElement);
        }

        // POST: api/Lab1DATC
        public IHttpActionResult Post([FromBody]KeyValuePair<int, string> value)
        {
            dataBaseLab1.Add(value.Key, value.Value);
            return Created(value.Key.ToString(), value);
        }

        // PUT: api/Lab1DATC/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Lab1DATC/5
        public void Delete(int id)
        {
        }
    }
}
