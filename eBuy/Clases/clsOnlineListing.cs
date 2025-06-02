using Antlr.Runtime.Tree;
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

        public object GetOnlineListingById(int IdOnlineListing)
        {
            try
            {
                var onlineListing = eBuyDB.OnlineListings
                    .Where(ol => ol.Id == IdOnlineListing && ol.IsActive)
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
                    }).FirstOrDefault();
                if (onlineListing == null)
                {
                    return new { Message = "Online listing not found or inactive." };
                }
                return onlineListing;
            }
            catch (Exception ex)
            {
                return new { Message = $"Error: {ex.Message}" };
            }
        }

        public string GetOnlineListingPublisherName(int idOnlineListing)
        {
            try
            {
                var IsOnlineListingOwn = eBuyDB.OnlineListingOwns.FirstOrDefault(olo => olo.IdOnlineListing == idOnlineListing);
                if (IsOnlineListingOwn != null)
                {
                    return IsOnlineListingOwn.EmployeeAssignedBranch;
                }
                var IsOnlineListingBySupplier = eBuyDB.OnlineListingBySuppliers.FirstOrDefault(ols => ols.IdOnlineListing == idOnlineListing);
                if (IsOnlineListingBySupplier != null)
                {
                    return IsOnlineListingBySupplier.SupplierName;
                }
                return "Publisher not found for this online listing.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";

            }
        }
    }
}