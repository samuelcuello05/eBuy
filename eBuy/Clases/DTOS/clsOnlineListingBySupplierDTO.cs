using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases.DTOS
{
    public class clsOnlineListingBySupplierDTO
    {

        public OnlineListing OnlineListing { get; set; }
        public Supplier Supplier { get; set; }
        public string ProductName { get; set; }
    }
}

