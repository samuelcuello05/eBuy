using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsPurchase
    {
        eBuyDBEntities eBuyDB = new eBuyDBEntities();

        List<string> paymentMethods = new List<string>
        {
            "Cash",
            "Credit Card",
            "Debit Card",
            "Bank Transfer"
        };

        public int GenerateInvoiceNumber()
        {
            var lastInvoice = eBuyDB.PurchaseInvoices.OrderByDescending(p => p.Id).FirstOrDefault();
            if (lastInvoice != null)
            {
                return lastInvoice.InvoiceNumber + 1;
            }
            return 1; // Si no existe un primer invoice, empieza en 1
        }

        public string InsertPurchase(int IdSupplier, string branchName, string paymentMethod, List<(Product product, int quantity)> productsToBuy)
        {
            try
            {
                using(var transaction = eBuyDB.Database.BeginTransaction())
                {
                    var supplier = eBuyDB.Suppliers.FirstOrDefault(s => s.Id == IdSupplier);
                    if (supplier == null)
                        return "Error: Supplier not found.";

                    if (productsToBuy == null || !productsToBuy.Any())
                        return "Error: No products provided for the purchase.";

                    var branch = eBuyDB.Branches.FirstOrDefault(b => b.Name.Equals(branchName, StringComparison.OrdinalIgnoreCase));
                    if (branch == null)
                        return "Error: Branch not found.";

                    bool matchFound = paymentMethods.Any(method => method.Equals(paymentMethod, StringComparison.OrdinalIgnoreCase));
                    if (!matchFound)
                        return "Error: Invalid payment method.";

                    Purchase purchase = new Purchase
                    {
                        IdSupplier = supplier.Id,
                        PurchaseDate = DateTime.Now,
                        PaymentStatus = "Completed",
                        PaymentMethod = paymentMethod,
                        PurchaseDetails = new List<PurchaseDetail>()
                    };

                    decimal totalAmount = 0;
                    clsBranch ClsBranch = new clsBranch();

                    foreach (var (product, quantity) in productsToBuy)
                    {
                        if (quantity <= 0)
                            return $"Error: Quantity for product {product.Name} must be greater than zero.";

                        int idProduct;
                        var productExists = eBuyDB.Products.FirstOrDefault(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase));

                        if (productExists == null)
                        {
                            var category = eBuyDB.Categories.FirstOrDefault(c => c.Name.Equals(product.Category.Name, StringComparison.OrdinalIgnoreCase));
                            if (category == null)
                                return $"Error: Category {product.Category.Name} not found.";
                            product.IdCategory = category.Id;

                            var brand = eBuyDB.Brands.FirstOrDefault(b => b.Name.Equals(product.Brand.Name, StringComparison.OrdinalIgnoreCase));
                            if (brand == null)
                                return $"Error: Brand {product.Brand.Name} not found.";
                            product.IdBrand = brand.Id;
                            product.Stock = quantity;


                            var newProduct = new Product
                            {
                                Name = product.Name,
                                Description = product.Description,
                                IdCategory = product.IdCategory,
                                IdBrand = product.IdBrand,
                                Stock = product.Stock,
                                CostPrice = product.CostPrice,
                                SalePrice = product.SalePrice,
                                WarrantyMonths = product.WarrantyMonths
                            };

                            eBuyDB.Products.Add(newProduct);
                            eBuyDB.SaveChanges();

                            idProduct = newProduct.Id;

                            var existingItem = eBuyDB.Inventories.FirstOrDefault(i => i.IdBranch == branch.Id && i.IdProduct == idProduct);
                            if (existingItem != null)
                            {
                                existingItem.CurrentStock += quantity;
                            }
                            else
                            {
                                Inventory newItem = new Inventory
                                {
                                    IdBranch = branch.Id,
                                    IdProduct = idProduct,
                                    CurrentStock = quantity
                                };
                                eBuyDB.Inventories.Add(newItem);
                            }
                            eBuyDB.SaveChanges();
                        }
                        else
                        {
                            productExists.Stock += quantity;
                            eBuyDB.SaveChanges();
                            idProduct = productExists.Id;

                            var existingItem = eBuyDB.Inventories.FirstOrDefault(i => i.IdBranch == branch.Id && i.IdProduct == productExists.Id);
                            if (existingItem != null)
                            {
                                existingItem.CurrentStock += quantity;
                            }
                            else
                            {
                                Inventory newItem = new Inventory
                                {
                                    IdBranch = branch.Id,
                                    IdProduct = idProduct,
                                    CurrentStock = quantity
                                };
                                eBuyDB.Inventories.Add(newItem);
                            }
                            eBuyDB.SaveChanges();
                        }

                        var productPurchased = eBuyDB.Products.FirstOrDefault(p => p.Id == idProduct);
                        decimal totalCost = productPurchased.CostPrice * quantity;

                        PurchaseDetail purchaseDetail = new PurchaseDetail
                        {
                            IdProduct = idProduct,
                            ProductName = productPurchased.Name,
                            Quantity = quantity,
                            UnitCost = productPurchased.CostPrice,
                            TotalCost = totalCost,
                            Purchase = purchase
                        };

                        purchase.PurchaseDetails.Add(purchaseDetail);
                        totalAmount += totalCost;
                    }
                    eBuyDB.SaveChanges();

                    purchase.TotalAmount = totalAmount;

                    eBuyDB.Purchases.Add(purchase);
                    eBuyDB.SaveChanges();

                    int invoiceNumber = GenerateInvoiceNumber();
                    PurchaseInvoice purchaseInvoice = new PurchaseInvoice
                    {
                        IdPurchase = purchase.Id,
                        TotalAmount = totalAmount,
                        InvoiceNumber = invoiceNumber,
                        PaymentMethod = purchase.PaymentMethod
                    };
                    eBuyDB.PurchaseInvoices.Add(purchaseInvoice);
                    eBuyDB.SaveChanges();

                    transaction.Commit();

                    return "Purchase added successfully";
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " Validation errors: ", fullErrorMessage);

                return "Error: " + exceptionMessage;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}