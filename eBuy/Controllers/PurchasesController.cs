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
    [RoutePrefix("api/Purchases")]
    public class PurchasesController : ApiController
    {
        [HttpPost]
        [Route("Insert")]
        public string InsertPurchase([FromBody] clsPurchaseRequestDto purchaseRequest)
        {
            clsPurchase purchase = new clsPurchase();

            var productsToBuy = purchaseRequest.ProductsToBuy.Select(pq =>
            (
                product: new Product
                {
                    Name = pq.Product.Name,
                    Description = pq.Product.Description,
                    CostPrice = pq.Product.CostPrice,
                    SalePrice = pq.Product.SalePrice,
                    WarrantyMonths = pq.Product.WarrantyMonths,
                    Category = new Category { Name = pq.Product.CategoryName},
                    Brand = new Brand { Name = pq.Product.BrandName }
                },
                quantity: pq.Quantity
                )).ToList();

            return purchase.InsertPurchase(purchaseRequest.IdSupplier, purchaseRequest.BranchName, purchaseRequest.PaymentMethod, productsToBuy);
        }
    }
}