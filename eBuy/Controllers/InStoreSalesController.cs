using eBuy.Clases;
using eBuy.Clases.DTOS;
using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/InStoreSales")]
    public class InStoreSalesController : ApiController
    {
        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public string InsertInStoreSale([FromBody] clsInStoreSaleDTO inStoreSaleRequest)
        {
            clsInStoreSale inStoreSale = new clsInStoreSale();
            var productsToSell = inStoreSaleRequest.ProductsToSell.Select(pq =>
            (
                product: new Product
                {
                    Name = pq.Product.Name
                },
                quantity: pq.Quantity
            )).ToList();
            return inStoreSale.InsertInStoreSale(inStoreSaleRequest.CustomerEmail, inStoreSaleRequest.PaymentMethod, inStoreSaleRequest.BranchName, productsToSell);
        }
    }
}