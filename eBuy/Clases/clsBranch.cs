using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsBranch
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public Branch branch { get; set; }

        public string InsertBranch()
        {
            try
            {
                eBuyDB.Branches.Add(branch);
                eBuyDB.SaveChanges();
                return "Branch added successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateBranch()
        {
            try
            {
                Branch Branch = SearchBranch(branch.Name);
                if (Branch == null)
                {
                    return "Branch not found";
                }
                eBuyDB.Branches.AddOrUpdate(branch);
                eBuyDB.SaveChanges();
                return "Branch updated successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public Branch SearchBranch(string name)
        {
            try
            {
                Branch Branch = eBuyDB.Branches.FirstOrDefault(b => string.Equals(b.Name, name, StringComparison.OrdinalIgnoreCase));
                return Branch;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public List<Branch> GetBranches()
        {
            try
            {
                return eBuyDB.Branches.OrderBy(b => b.Name).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public string DeleteBranch(string name)
        {
            try
            {
                Branch Branch = SearchBranch(name);
                if (Branch == null)
                {
                    return "Branch not found";
                }
                eBuyDB.Branches.Remove(Branch);
                eBuyDB.SaveChanges();
                return "Branch deleted successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateOrAddItemToInventory(string branchName, int IdProduct, int quantity)
        {
            try
            {

                var branch = eBuyDB.Branches.FirstOrDefault(b => b.Name.Equals(branchName, StringComparison.OrdinalIgnoreCase));
                if (branch == null)
                {
                    return "Error: Branch not found";
                }

                var existingItem = eBuyDB.Inventories.FirstOrDefault(i => i.IdBranch == branch.Id && i.IdProduct == IdProduct);
                if (existingItem != null)
                {
                    existingItem.CurrentStock += quantity;
                }
                else
                {
                    Inventory newItem = new Inventory
                    {
                        IdBranch = branch.Id,
                        IdProduct = IdProduct,
                        CurrentStock = quantity
                    };
                    eBuyDB.Inventories.Add(newItem);
                }

                return "Item added to inventory successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateInventory(string branchName, string productName, int quantity)
        {
            try
            {
                var branch = eBuyDB.Branches.FirstOrDefault(b => b.Name.Equals(branchName, StringComparison.OrdinalIgnoreCase));
                if (branch == null)
                {
                    return "Error: Branch not found";
                }
                var product = eBuyDB.Products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
                if (product == null)
                {
                    return "Error: Product not found";
                }
                var existingItem = eBuyDB.Inventories.FirstOrDefault(i => i.IdBranch == branch.Id && i.IdProduct == product.Id);
                if (existingItem != null)
                {
                    existingItem.CurrentStock = quantity;
                    eBuyDB.SaveChanges();
                    return "Inventory updated successfully";
                }
                else
                {
                    return "Error: Inventory item not found";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string MoveEmployeeBetweenBranches(int employeeId, string newBranchName)
        {
            try
            {
                var employee = eBuyDB.Employees.Find(employeeId);
                if (employee == null)
                {
                    return "Error: Employee not found";
                }
                var newBranch = eBuyDB.Branches.FirstOrDefault(b => b.Name.Equals(newBranchName, StringComparison.OrdinalIgnoreCase));
                if (newBranch == null)
                {
                    return "Error: Branch not found";
                }
                employee.IdBranch = newBranch.Id;
                eBuyDB.SaveChanges();
                return "Employee moved to new branch successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}