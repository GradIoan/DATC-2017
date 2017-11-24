using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lab1.Controllers
{
    public class ValuesController : ApiController
    {
        private static List<string> objList = new List<string>();
        
        // GET api/values
        public IEnumerable<string> Get()
        {
            return objList.ToArray();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            objList.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            try
            {
                objList.RemoveAt(id);
            }
            catch
            {
                // if id is bigger than list length, or smaller than 0
            }
        }
    }
}
