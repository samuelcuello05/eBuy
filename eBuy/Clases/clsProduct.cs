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
                return "Error: " + ex.Message;
            }
        }

        public string UpdateProduct()
        {
            try
            {
                Product Product = SearchProduct(product.Name);
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

        public Product SearchProduct(string name)
        {
            try
            {
                Product Product = eBuyDB.Products.FirstOrDefault(p => string.Equals(p.Name,name, StringComparison.OrdinalIgnoreCase));
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

        public string DeleteProduct(string name)
        {
            try
            {
                Product Product = SearchProduct(name);
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