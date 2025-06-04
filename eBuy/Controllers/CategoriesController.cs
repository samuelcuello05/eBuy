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
    [RoutePrefix("api/Categories")]
    public class CategoriesController : ApiController
    {
        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public string InsertCategory([FromBody] Category category)
        {
            clsCategory Category = new clsCategory();
            Category.category = category;
            return Category.InsertCategory();
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public string UpdateCategory([FromBody] Category category)
        {
            clsCategory Category = new clsCategory();
            Category.category = category;
            return Category.UpdateCategory();
        }

        [HttpGet]
        [Route("Search")]
        [AllowAnonymous]
        public Category SearchCategory(string name)
        {
            clsCategory Category = new clsCategory();
            return Category.SearchCategory(name);
        }

        [HttpGet]
        [Route("List")]
        [AllowAnonymous]
        public List<Category> ListCategories()
        {
            clsCategory Category = new clsCategory();
            return Category.ListCategories();
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public string DeleteCategory(string name)
        {
            clsCategory Category = new clsCategory();
            return Category.DeleteCategory(name);   
        }
    }
}