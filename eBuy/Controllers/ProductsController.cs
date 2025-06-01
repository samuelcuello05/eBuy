using eBuy.Clases;
using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        [HttpPost]
        [Route("Insert")]
        public string InsertProduct([FromBody] Product product)
        {
            clsProduct Product = new clsProduct();
            Product.product = product;
            return Product.InsertProduct();
        }

        [HttpPut]
        [Route("Update")]
        public string UpdateProduct([FromBody] Product product)
        {
            clsProduct Product = new clsProduct();
            Product.product = product;
            return Product.UpdateProduct();
        }

        [HttpGet]
        [Route("Search")]
        public Product SearchProduct(int id)
        {
            clsProduct Product = new clsProduct();
            return Product.SearchProduct(id);
        }

        [HttpGet]
        [Route("List")]
        public List<Product> SearchAllProduct()
        {
            clsProduct Product = new clsProduct();
            return Product.ListProducts();
        }

        [HttpDelete]
        [Route("Delete")]
        public string DeleteProduct(int id)
        {
            clsProduct Product = new clsProduct();
            return Product.DeleteProduct(id);
        }
    }
}