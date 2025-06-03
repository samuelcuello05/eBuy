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
    [RoutePrefix("api/OnlineSales")]
    public class OnlineSalesController : ApiController
    {
        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public string InsertOnlineSale([FromBody] clsOnlineSaleDTO onlineSaleRequest)
        {
            clsOnlineSale onlineSale = new clsOnlineSale();

            var productsToSell = onlineSaleRequest.ProductsToSell.Select(pq =>
            (
                product: new Product
                {
                    Name = pq.Product.Name
                },
                quantity: pq.Quantity,
                branchName: pq.BranchName
            )).ToList();
            return onlineSale.InsertOnlineSale(
                onlineSaleRequest.IdCustomer,
                onlineSaleRequest.ShippingAddress,
                onlineSaleRequest.PaymentMethod,
                productsToSell
            );
        }
    }
}