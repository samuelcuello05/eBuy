using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases.DTOS
{
    public class clsOnlineSaleDTO
    {
        public int IdCustomer { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; } // "Cash", "Credit Card", "Debit Card", "Bank Transfer"
        public List<ProductWithQuantityToSellDto> ProductsToSell { get; set; }
    }
    public class ProductWithQuantityToSellDto
    {
        public ProductToSellDto Product { get; set; }
        public int Quantity { get; set; }
        public string BranchName { get; set; }
    }

    public class ProductToSellDto
    {
        public string Name { get; set; }
    }
}