using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace eBuy.Clases
{
    public class clsCategory
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public Category category { get; set; }

        public string InsertCategory()
        {
            try
            {
                if (category.Description.Length > 300)
                    return "Error: description contains more than 300 characters";
                eBuyDB.Categories.Add(category);
                eBuyDB.SaveChanges();
                return "Category added successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string UpdateCategory()
        {
            try
            {
                Category Category = SearchCategory(category.Name);
                if (Category == null)
                {
                    return "Category not found";
                }
                eBuyDB.Categories.AddOrUpdate(category);
                eBuyDB.SaveChanges();
                return "Category updated successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public Category SearchCategory(string name)
        {
            try
            {
                Category Category = eBuyDB.Categories.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
                return Category;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public List<Category> ListCategories()
        {
            try
            {
                return eBuyDB.Categories.OrderBy(c => c.Name).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public string DeleteCategory(string name)
        {
            try
            {
                Category Category = SearchCategory(name);
                if (Category == null)
                {
                    return "Category not found";
                }
                eBuyDB.Categories.Remove(Category);
                eBuyDB.SaveChanges();
                return "Category deleted successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}