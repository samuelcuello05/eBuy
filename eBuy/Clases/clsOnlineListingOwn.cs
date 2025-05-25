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

        public string AddOnlineListingOwn(OnlineListing OnlineListing, Employee employee, string productName)
        {
            try
            {
                using (var transaction = eBuyDB.Database.BeginTransaction())
                {
                    if (OnlineListing == null)
                        return "Error: Online listing data is missing.";

                    if (employee == null)
                        return "Error: Employee data is missing.";

                    var employeeExists = eBuyDB.Employees.FirstOrDefault(e => e.Id == employee.Id);
                    if (employeeExists == null)
                    {
                        return "Error: Employee not found";
                    }

                    var product = eBuyDB.Products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
                    if (product == null)
                    {
                        return "Error: Product not found";
                    }

                    OnlineListing.IdProduct = product.Id;
                    OnlineListing.CreatedAt = DateTime.Now;
                    OnlineListing.UpdatedAt = DateTime.Now;
                    OnlineListing.IsActive = true;
                    eBuyDB.OnlineListings.Add(OnlineListing);
                    eBuyDB.SaveChanges();

                    OnlineListingOwn newOnlineListingOwn = new OnlineListingOwn
                    {
                        IdOnlineListing = OnlineListing.Id,
                        IdEmployee = employee.Id,
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

    }
}