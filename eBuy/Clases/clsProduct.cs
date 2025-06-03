using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsProduct
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public Product product { get; set; }
        public string InsertProduct()
        {
            try
            {
                eBuyDB.Products.Add(product);
                eBuyDB.SaveChanges();
                return "Product added successfully";
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;

                if (ex.InnerException != null)
                {
                    errorMessage += " | Inner: " + ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                    {
                        errorMessage += " | Inner2: " + ex.InnerException.InnerException.Message;
                    }
                }

                return "Error: " + errorMessage;
            }
        }

        public string UpdateProduct()
        {
            try
            {
                Product Product = SearchProduct(product.Id);
                if (Product == null)
                {
                    return "Product not found";
                }
                eBuyDB.Products.AddOrUpdate(product);
                eBuyDB.SaveChanges();
                return "Product updated successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public Product SearchProduct(int id)
        {
            try
            {
                Product Product = eBuyDB.Products.FirstOrDefault(p => p.Id == id);
                return Product;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public List<Product> ListProducts()
        {
            try
            {
                return eBuyDB.Products.OrderBy(p => p.Name).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public List<Product> ListProductByBranch(string branchName)
        {
            try
            {
                var branch = eBuyDB.Branches
                    .FirstOrDefault(b => b.Name.Equals(branchName, StringComparison.OrdinalIgnoreCase));

                if (branch == null)
                {
                    return new List<Product>();
                }

                var productsWithStock = (from inventory in eBuyDB.Inventories
                                         where inventory.IdBranch == branch.Id && inventory.CurrentStock > 0
                                         join product in eBuyDB.Products on inventory.IdProduct equals product.Id
                                         orderby product.Name
                                         select product).ToList();

                return productsWithStock;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public string DeleteProduct(int id)
        {
            try
            {
                Product Product = SearchProduct(id);
                if (Product == null)
                {
                    return "Product not found";
                }
                eBuyDB.Products.Remove(Product);
                eBuyDB.SaveChanges();
                return "Product deleted successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}