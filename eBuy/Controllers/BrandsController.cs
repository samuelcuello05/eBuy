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
    [RoutePrefix("api/Brands")]
    public class BrandsController : ApiController
    {
        
        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public string InsertBrand([FromBody] Brand brand)
        {
            clsBrand Brand = new clsBrand();
            Brand.brand = brand;
            return Brand.InsertBrand();
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public string UpdateBrand([FromBody] Brand brand)
        {
            clsBrand Brand = new clsBrand();
            Brand.brand = brand;
            return Brand.UpdateBrand();
        }

        [HttpGet]
        [Route("Search")]
        [Authorize]
        public Brand SearchBrand(string name)
        {
            clsBrand Brand = new clsBrand();
            return Brand.SearchBrand(name); 
        }

        [HttpGet]
        [Route("List")]
        [Authorize]
        public List<Brand> SearchAllBrands()
        {
            clsBrand Brand = new clsBrand();
            return Brand.ListBrands();
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public string DeleteBrand(string name)
        {
            clsBrand Brand = new clsBrand();
            return Brand.DeleteBrand(name);
        }
    }
}