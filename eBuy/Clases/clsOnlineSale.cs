using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsOnlineSale
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();

        List<string> paymentMethods = new List<string>
        {
            "Cash",
            "Credit Card",
            "Debit Card",
            "Bank Transfer"
        };

        public int GenerateTrackingNumber()
        {
            var lastTrackingNumber = eBuyDB.OnlineSales.OrderByDescending(p => p.Id).FirstOrDefault();
            if (lastTrackingNumber != null)
            {
                return lastTrackingNumber.TrackingNumber + 1;
            }
            return 1;
        }

        public int GenerateInvoiceNumber()
        {
            var lastInvoice = eBuyDB.OnlineSaleInvoices.OrderByDescending(p => p.Id).FirstOrDefault();
            if (lastInvoice != null)
            {
                return lastInvoice.InvoiceNumber + 1;
            }
            return 1;
        }

        public int GenerateReferenceNumber()
        {
            var lastReferenceNumber = eBuyDB.OnlinePayments.OrderByDescending(p => p.Id).FirstOrDefault();
            if (lastReferenceNumber != null)
            {
                return lastReferenceNumber.ReferenceNumber + 1;
            }
            return 1;
        }

        public string InsertOnlineSale(int IdCustomer, string shippingAddress,string paymentMethod, string branchName, List<(Product product, int quantity)> productsToSell)
        {
            try
            {
                using (var transaction = eBuyDB.Database.BeginTransaction())
                {
                    var customer = eBuyDB.Customers.FirstOrDefault(c => c.Id == IdCustomer);
                    if (customer == null)
                        return "Error: Customer not found.";

                    if (productsToSell == null || !productsToSell.Any())
                        return "Error: No products provided for the sale.";

                    if (string.IsNullOrWhiteSpace(shippingAddress))
                        return "Error: Shipping address is required.";

                    var branch = eBuyDB.Branches.FirstOrDefault(b => b.Name.Equals(branchName, StringComparison.OrdinalIgnoreCase));
                    if (branch == null)
                        return "Error: Branch not found.";

                    bool matchFound = paymentMethods.Any(method => method.Equals(paymentMethod, StringComparison.OrdinalIgnoreCase));
                    if (!matchFound)
                        return "Error: Invalid payment method.";

                    decimal totalAmount = 0;
                    clsBranch ClsBranch = new clsBranch();

                    foreach (var (product, quantity) in productsToSell)
                    {
                        var productExists = eBuyDB.Products.FirstOrDefault(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase));
                        if (productExists == null)
                            return $"Error: Product {product.Name} not found.";

                        if (productExists.Stock < quantity)
                            return $"Error: Not enough stock for product {product.Name}.";
                    }

                    Sale sale = new Sale
                    {
                        IdCustomer = customer.Id,
                        SaleDate = DateTime.Now,
                        Status = "Successful",
                        PaymentMethod = paymentMethod,
                        TotalAmount = 0
                    };
                    eBuyDB.Sales.Add(sale);
                    eBuyDB.SaveChanges();

                    OnlineSale onlineSale = new OnlineSale
                    {
                        IdSale = sale.Id,
                        ShippingAddress = shippingAddress,
                        PaymentStatus = true,
                        TrackingNumber = GenerateTrackingNumber()
                    };
                    eBuyDB.OnlineSales.Add(onlineSale);
                    eBuyDB.SaveChanges();

                    foreach (var (product, quantity) in productsToSell)
                    {
                        var productExists = eBuyDB.Products.First(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase));
                        productExists.Stock -= quantity;

                        var existingItem = eBuyDB.Inventories.FirstOrDefault(i => i.IdBranch == branch.Id && i.IdProduct == productExists.Id);
                        if (existingItem != null)
                        {
                            existingItem.CurrentStock -= quantity;
                            eBuyDB.SaveChanges();
                        }
                        else
                        {
                            return $"Error: Inventory item not found for branch {branchName}";
                        }

                        decimal itemTotal = productExists.SalePrice * quantity;
                        totalAmount += itemTotal;

                        OnlineSaleDetail detail = new OnlineSaleDetail
                        {
                            IdOnlineSale = onlineSale.Id,
                            IdProduct = productExists.Id,
                            ProductName = productExists.Name,
                            Quantity = quantity,
                            UnitPrice = productExists.SalePrice,
                            TotalPrice = itemTotal
                        };
                        eBuyDB.OnlineSaleDetails.Add(detail);
                        eBuyDB.SaveChanges();

                        OnlineWarranty warranty = new OnlineWarranty
                        {
                            IdOnlineSaleDetail = detail.Id,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddMonths(productExists.WarrantyMonths),
                            WarrantyStatus = "Active"
                        };
                        eBuyDB.OnlineWarranties.Add(warranty);
                        eBuyDB.SaveChanges();
                    }
                    sale.TotalAmount = totalAmount;
                    eBuyDB.Entry(sale).State = EntityState.Modified;

                    OnlineSaleInvoice invoice = new OnlineSaleInvoice
                    {
                        IdOnlineSale = onlineSale.Id,
                        InvoiceNumber = GenerateInvoiceNumber(),
                        TotalAmount = totalAmount,
                        PaymentMethod = paymentMethod
                    };
                    eBuyDB.OnlineSaleInvoices.Add(invoice);
                    eBuyDB.SaveChanges();

                    OnlinePayment payment = new OnlinePayment
                    {
                        IdOnlineSaleInvoice = invoice.Id,
                        PaymentDate = DateTime.Now,
                        Amount = totalAmount,
                        PaymentMethod = paymentMethod,
                        ReferenceNumber = GenerateReferenceNumber(),
                        PaymentStatus = "Completed"
                    };
                    eBuyDB.OnlinePayments.Add(payment);
                    eBuyDB.SaveChanges();

                    transaction.Commit();

                    return "Online sale inserted successfully.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}