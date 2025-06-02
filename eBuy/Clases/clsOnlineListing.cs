using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsOnlineListing
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();

        public IEnumerable<object> ListOnlineListing()
        {
            try
            {
                var onlineListings = eBuyDB.OnlineListings
                    .Where(ol => ol.IsActive)
                    .Select(ol => new
                    {
                        ol.Id,
                        ol.IdProduct,
                        ol.Title,
                        ol.Description,
                        ol.Price,
                        ol.AvailableQuantity,
                        ol.CreatedAt,
                        ol.UpdatedAt,
                        ProductName = eBuyDB.Products.FirstOrDefault(p => p.Id == ol.IdProduct).Name
                    }).ToList();
                return onlineListings;
            }
            catch (Exception ex)
            {
                return new List<object> { $"Error: {ex.Message}" };
            }
        }
    }
}