using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsBrand
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public Brand brand { get; set; }

        public string InsertBrand()
        {
            try
            {
                eBuyDB.Brands.Add(brand);
                eBuyDB.SaveChanges();
                return "Brand added successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateBrand()
        {
            try
            {
                Brand Brand = SearchBrand(brand.Name);
                if (Brand == null)
                {
                    return "Brand not found";
                }
                eBuyDB.Brands.AddOrUpdate(brand);
                eBuyDB.SaveChanges();
                return "Brand updated successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public Brand SearchBrand(string name)
        {
            try
            {
                Brand Brand = eBuyDB.Brands.FirstOrDefault(b => string.Equals(b.Name, name, StringComparison.OrdinalIgnoreCase));
                return Brand;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public List<Brand> ListBrands()
        {
            try
            {
                return eBuyDB.Brands.OrderBy(b => b.Name).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public string DeleteBrand(string name)
        {
            try
            {
                Brand Brand = SearchBrand(name);
                if (Brand == null)
                {
                    return "Brand not found";
                }
                eBuyDB.Brands.Remove(Brand);
                eBuyDB.SaveChanges();
                return "Brand deleted successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}