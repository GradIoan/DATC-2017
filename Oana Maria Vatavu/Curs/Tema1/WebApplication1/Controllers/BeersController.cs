using System;
using System.Collections.Concurrent;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using WebApplication1.Models;

namespace BeerApi.Controllers
{
    [RoutePrefix("api/beers")]
    public class BeersController : ApiController
    {
        
        private static ConcurrentDictionary<string, Beer> _beers = new ConcurrentDictionary<string, Beer>();

        public object Conversation { get; private set; }

        [Route("{id}", Name = "GetById")]
        public IHttpActionResult Get(string id)
        {
            Beer beer = null;
            if (_beers.TryGetValue(id, out beer))
            {
                return Ok(beer);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(Beer beer)
        {
            if (beer == null)
            {
                return BadRequest("Beer cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            beer.Id = Guid.NewGuid().ToString();
            _beers[beer.Id] = beer;
            return CreatedAtRoute("GetById", new { id = beer.Id }, beer);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(string id, Beer beer)
        {
            if (beer == null)
            {
                return BadRequest("Beer cannot be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (beer.Id != id)
            {
                return BadRequest("beer.id does not match id parameter");
            }

            if (!_beers.Keys.Contains(id))
            {
                return NotFound();
            }

            _beers[id] = beer;
            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            Beer beer = null;
            _beers.TryRemove(id, out beer);
            return new StatusCodeResult(HttpStatusCode.NoContent, this);
        }
    }
}
