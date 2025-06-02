using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsProductImage
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();

        public string IdProduct { get; set; }
        public List<String> Archives { get; set; }

        public string SaveImages()
        {
            try
            {
                if (Archives.Count > 0)
                {
                    foreach (var archive in Archives)
                    {
                        ProductImage productImage = new ProductImage();
                        productImage.IdProduct = Convert.ToInt32(IdProduct);
                        productImage.Name = archive;
                        eBuyDB.ProductImages.Add(productImage);
                        eBuyDB.SaveChanges();
                    }
                    return "Images saved successfully.";
                }
                else
                {
                    return "No images to save.";
                }
            }
            catch (Exception ex)
            {
                return "Error saving image: " + ex.Message;
            }
        }

        public string DeleteImage(string ImageName)
        {
            try 
            {
                ProductImage productImage = eBuyDB.ProductImages.FirstOrDefault(pi => pi.Name == ImageName);
                if(productImage != null)
                {
                    eBuyDB.ProductImages.Remove(productImage);
                    eBuyDB.SaveChanges();
                    return "Image deleted successfully.";
                }
                else
                {
                    return "Image not found.";
                }
            }
            catch (Exception ex)
            {
                return "Error deleting image: " + ex.Message;
            }
        }

        public List<string> GetImagesByProductId(int IdProduct)
        {
            try
            {
                var productExists = eBuyDB.Products.Any(p => p.Id == IdProduct);

                if (!productExists)
                {
                    return new List<string>();
                }

                var imageNames = eBuyDB.ProductImages
                    .Where(pi => pi.IdProduct == IdProduct)
                    .Select(pi => pi.Name)
                    .ToList();

                return imageNames;
            }
            catch (Exception ex)
            {
                return new List<string> { $"Error: {ex}" };
            }
        }


    }
}