using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsSale
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public Sale sale { get; set; }

        public List<Sale> GetSales() 
        {
            try
            {
                return eBuyDB.Sales.OrderBy(b => b.SaleDate).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}