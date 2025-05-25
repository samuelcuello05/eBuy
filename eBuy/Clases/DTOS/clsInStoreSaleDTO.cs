using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases.DTOS
{
    public class clsInStoreSaleDTO
    {
        public string CustomerEmail { get; set; }
        public string PaymentMethod { get; set; } // "Cash", "Credit Card", "Debit Card", "Bank Transfer"
        public string BranchName { get; set; }
        public List<ProductWithQuantityToInStoreSellDto> ProductsToSell { get; set; }
    }

    public class ProductWithQuantityToInStoreSellDto
    {
        public ProductToInStoreSellDto Product { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductToInStoreSellDto
    {
        public string Name { get; set; }
    }
}