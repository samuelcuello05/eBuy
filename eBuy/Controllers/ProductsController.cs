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
        [Authorize]
        public string InsertProduct([FromBody] Product product)
        {
            clsProduct Product = new clsProduct();
            Product.product = product;
            return Product.InsertProduct();
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public string UpdateProduct([FromBody] Product product)
        {
            clsProduct Product = new clsProduct();
            Product.product = product;
            return Product.UpdateProduct();
        }

        [HttpGet]
        [Route("Search")]
        [AllowAnonymous]
        public Product SearchProduct(int id)
        {
            clsProduct Product = new clsProduct();
            return Product.SearchProduct(id);
        }

        [HttpGet]
        [Route("List")]
        [AllowAnonymous]
        public List<Product> SearchAllProduct()
        {
            clsProduct Product = new clsProduct();
            return Product.ListProducts();
        }

        [HttpGet]
        [Route("ListByBranchName")]
        [AllowAnonymous]
        public List<Product> ListProductByBranch(string branchName)
        {
            clsProduct Product = new clsProduct();
            return Product.ListProductByBranch(branchName);
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public string DeleteProduct(int id)
        {
            clsProduct Product = new clsProduct();
            return Product.DeleteProduct(id);
        }
    }
}