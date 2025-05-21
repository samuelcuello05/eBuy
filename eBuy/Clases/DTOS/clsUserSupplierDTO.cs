using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases.DTOS
{
    public class clsUserSupplierDTO
    {
        public User User { get; set; }
        public Supplier Supplier { get; set; }
    }
}