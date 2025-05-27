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
        public string InsertCategory([FromBody] Category category)
        {
            clsCategory Category = new clsCategory();
            Category.category = category;
            return Category.InsertCategory();
        }

        [HttpPut]
        [Route("Update")]
        public string UpdateCategory([FromBody] Category category)
        {
            clsCategory Category = new clsCategory();
            Category.category = category;
            return Category.UpdateCategory();
        }

        [HttpGet]
        [Route("Search")]
        public Category SearchCategory(string name)
        {
            clsCategory Category = new clsCategory();
            return Category.SearchCategory(name);
        }

        [HttpGet]
        [Route("List")]
        public List<Category> ListlCategory()
        {
            clsCategory Category = new clsCategory();
            return Category.ListCategories();
        }

        [HttpDelete]
        [Route("Delete")]
        public string DeleteCategory(string name)
        {
            clsCategory Category = new clsCategory();
            return Category.DeleteCategory(name);   
        }
    }
}