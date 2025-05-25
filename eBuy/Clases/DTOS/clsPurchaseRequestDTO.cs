using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases.DTOS
{
    public class clsPurchaseRequestDto
    {
        public int IdSupplier { get; set; }
        public string BranchName { get; set; }
        public string PaymentMethod { get; set; } // "Cash", "Credit Card", "Debit Card", "Bank Transfer"
        public List<ProductWithQuantityToPurchaseDto> ProductsToBuy { get; set; }
    }

    public class ProductWithQuantityToPurchaseDto
    {
        public ProductToPurchaseDto Product { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductToPurchaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }  // nombre de la categoría, se buscará el Id
        public string BrandName { get; set; }     // nombre de la marca, se buscará el Id
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int WarrantyMonths { get; set; }
    }
}