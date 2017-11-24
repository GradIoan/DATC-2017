using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ProductsController : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }
        public Product GetProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return item;
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(p => string.Equals(p.Category, category, System.StringComparison.OrdinalIgnoreCase));
        }
        public HttpResponseMessage PostProduct(Product item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaulApi", new { id = item.Id });
            response.Headers.Location = new System.Uri(uri);
            return response;
        }
        public void PutProduct(int id, Product product)
        {
            product.Id = id;
            if(!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        public void DeleteProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            repository.Remove(id);
        }
    }
}
