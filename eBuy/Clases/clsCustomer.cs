using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsCustomer
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public Customer customer { get; set; }
    }
}