using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplicationAR.Models;

namespace WebApplicationAR.Controllers
{
    public class ProductsController : ApiController
    {

        ProductRepository _repository = new ProductRepository();

        [Route("api/Product")]
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _repository.Get();
        }

        [Route("api/Product/{id}")]
        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var response = _repository.GetProduct(id);
            if (response != null) {
                return Ok(response);
            }
            return NotFound();
        }

        [Route("api/Category/{category}/{type}")]
        [HttpGet]
        public IHttpActionResult GetProductByCategory(int category, int type)
        {
            var response = _repository.GetProductByCategory(category, type);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        [Route("api/Name/{name}")]
        [HttpGet]
        public IHttpActionResult GetProductByName(string name)
        {
            var response = _repository.GetProductByName(name);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }


        [Route("api/Desc/{desc}")]
        [HttpGet]
        public IHttpActionResult GetProductByDescripcion(string desc)
        {
            var response = _repository.GetProductByDescripcion(desc);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPut]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            var response = _repository.PutProduct(id, product);

            if(response == null)
            {
                return NotFound();

            }    
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [HttpPost]
        public IHttpActionResult PostProduct(Product product)
        {
            var response = _repository.PostProduct(product);

            return Ok(response);
        }

        [Route("api/product/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var response = _repository.DeleteProduct(id);
            return Ok(response);
        }

    }
}