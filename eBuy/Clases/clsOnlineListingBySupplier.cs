using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsOnlineListingBySupplier
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();

        public string AddOnlineListingBySupplier(OnlineListing OnlineListing, int IdSupplier, string productName)
        {
            try
            {
                using (var transaction = eBuyDB.Database.BeginTransaction())
                {

                    if (OnlineListing == null)
                        return "Error: Online listing data is missing.";

                    var supplierExists = eBuyDB.Suppliers.FirstOrDefault(s => s.Id == IdSupplier);
                    if (supplierExists == null)
                    {
                        return "Error: Supplier not found";
                    }

                    var product = eBuyDB.Products.FirstOrDefault(p =>p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
                    if (product == null)
                    {
                        return "Error: Product not found";
                    }

                    OnlineListing.Title = product.Name;
                    OnlineListing.IdProduct = product.Id;
                    OnlineListing.CreatedAt = DateTime.Now;
                    OnlineListing.UpdatedAt = DateTime.Now;
                    OnlineListing.IsActive = true;
                    eBuyDB.OnlineListings.Add(OnlineListing);
                    eBuyDB.SaveChanges();

                    OnlineListingBySupplier newOnlineListingBySupplier = new OnlineListingBySupplier
                    {
                        IdOnlineListing = OnlineListing.Id,
                        IdSupplier = supplierExists.Id,
                        SupplierName = supplierExists.Name,
                        SupplierEmail = eBuyDB.Users.FirstOrDefault(u => u.Id == supplierExists.IdUser).Email,
                        SupplierPhone = supplierExists.Phone
                    };

                    eBuyDB.OnlineListingBySuppliers.Add(newOnlineListingBySupplier);
                    eBuyDB.SaveChanges();

                    transaction.Commit();


                    return "Online listing by supplier successfully registered.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public IEnumerable<object> ListOnlineListingBySupplier(int IdSupplier)
        {
            try
            {
                var listings = eBuyDB.OnlineListingBySuppliers
                    .Where(ols => ols.IdSupplier == IdSupplier)
                    .Select(ols => new
                    {
                        ols.Id,
                        ols.OnlineListing.IdProduct,
                        ols.OnlineListing.Title,
                        ols.OnlineListing.Description,
                        ols.OnlineListing.Price,
                        ols.OnlineListing.AvailableQuantity,
                        ols.OnlineListing.IsActive,
                        ols.OnlineListing.CreatedAt,
                        ols.OnlineListing.UpdatedAt,
                        ols.SupplierName,
                        ols.SupplierEmail,
                        ols.SupplierPhone
                    })
                    .ToList();

                return listings;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new List<object>();
            }
        }

        public string ModifyOnlineListingBySupplier(OnlineListing updatedListing)
        {
            try
            {
                var existingListing = eBuyDB.OnlineListings.Find(updatedListing.Id);
                if (existingListing == null)
                    return "Error: Online listing not found.";

                existingListing.Title = updatedListing.Title;
                existingListing.Description = updatedListing.Description;
                existingListing.Price = updatedListing.Price;
                existingListing.AvailableQuantity = updatedListing.AvailableQuantity;
                existingListing.IsActive = updatedListing.IsActive;
                existingListing.UpdatedAt = DateTime.Now;
                eBuyDB.SaveChanges();

                return "Online listing updated successfully.";

            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string DeleteOnlineListingBySupplier(int id)
        {
            try
            {
                using (var transaction = eBuyDB.Database.BeginTransaction())
                {
                    var listingBySupplier = eBuyDB.OnlineListingBySuppliers.Find(id);
                    if (listingBySupplier == null)
                        return "Error: Online listing by supplier not found.";

                    var listing = eBuyDB.OnlineListings.Find(listingBySupplier.IdOnlineListing);
                    if (listing == null)
                        return "Error: Associated online listing not found.";

                    eBuyDB.OnlineListingBySuppliers.Remove(listingBySupplier);
                    eBuyDB.OnlineListings.Remove(listing);

                    eBuyDB.SaveChanges();
                    transaction.Commit();

                    return "Online listing deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

    }
}