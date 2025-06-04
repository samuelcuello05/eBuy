using eBuy.Clases;
using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/Sales")]
    public class SalesController : ApiController
    {
        [HttpGet]
        [Route("ListSales")]
        [Authorize]
        public List<Sale> ListSales()
        {
            clsSale Sales = new clsSale();
            return Sales.GetSales();
        }
    }
}