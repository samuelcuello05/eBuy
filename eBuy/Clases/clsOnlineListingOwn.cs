using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Util;

namespace eBuy.Clases
{
    public class clsOnlineListingOwn
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();

        public string AddOnlineListingOwn(int IdEmployee, string productName)
        {
            try
            {
                using (var transaction = eBuyDB.Database.BeginTransaction())
                {

                    var employeeExists = eBuyDB.Employees.FirstOrDefault(e => e.Id == IdEmployee);
                    if (employeeExists == null)
                    {
                        return "Error: Employee not found";
                    }

                    var product = eBuyDB.Products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
                    if (product == null)
                    {
                        return "Error: Product not found";
                    }


                    OnlineListing OnlineListing = new OnlineListing{
                        IdProduct = product.Id,
                        Title = productName,
                        Description = product.Description,
                        Price = product.SalePrice,
                        AvailableQuantity = product.Stock,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    eBuyDB.OnlineListings.Add(OnlineListing);
                    eBuyDB.SaveChanges();

                    OnlineListingOwn newOnlineListingOwn = new OnlineListingOwn
                    {
                        IdOnlineListing = OnlineListing.Id,
                        IdEmployee = employeeExists.Id,
                        EmployeeName = employeeExists.Name,
                        EmployeeAssignedBranch = employeeExists.AssignedBranch
                    };
                    eBuyDB.OnlineListingOwns.Add(newOnlineListingOwn);
                    eBuyDB.SaveChanges();

                    transaction.Commit();

                    return "Online listing own successfully registered.";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public IEnumerable<object> ListOnlineListingOwn()
        {
            try
            {
                var listings = eBuyDB.OnlineListingOwns
                .Select(olo => new
                {
                    olo.Id,
                    olo.OnlineListing.IdProduct,
                    olo.OnlineListing.Title,
                    olo.OnlineListing.Description,
                    olo.OnlineListing.Price,
                    olo.OnlineListing.AvailableQuantity,
                    olo.OnlineListing.IsActive,
                    olo.OnlineListing.CreatedAt,
                    olo.OnlineListing.UpdatedAt,
                    olo.IdEmployee,
                    olo.EmployeeName,
                    olo.EmployeeAssignedBranch
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
    }
}