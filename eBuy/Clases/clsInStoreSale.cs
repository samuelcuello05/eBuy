using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsInStoreSale
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();

        List<string> paymentMethods = new List<string>
        {
            "Cash",
            "Credit Card",
            "Debit Card",
            "Bank Transfer"
        };
        public int GenerateInvoiceNumber()
        {
            var lastInvoice = eBuyDB.InStoreSaleInvoices.OrderByDescending(p => p.Id).FirstOrDefault();
            if (lastInvoice != null)
            {
                return lastInvoice.InvoiceNumber + 1;
            }
            return 1;
        }

        public int GenerateReferenceNumber()
        {
            var lastReferenceNumber = eBuyDB.InStorePayments.OrderByDescending(p => p.Id).FirstOrDefault();
            if (lastReferenceNumber != null)
            {
                return lastReferenceNumber.ReferenceNumber + 1;
            }
            return 1;
        }

        public string InsertInStoreSale(string customerEmail, string paymentMethod, string branchName, List<(Product product, int quantity)> productsToSell)
        {
            try
            {
                using (var transaction = eBuyDB.Database.BeginTransaction())
                {
                    var customer = eBuyDB.Users.FirstOrDefault(u => u.Email.Equals(customerEmail, StringComparison.OrdinalIgnoreCase));
                    if (customer == null)
                        return "Error: Customer not found.";

                    if (productsToSell == null || !productsToSell.Any())
                        return "Error: No products provided for the sale.";

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

                    InStoreSale inStoreSale = new InStoreSale
                    {
                        IdSale = sale.Id,
                        IdBranch = branch.Id,
                        BranchName = branch.Name
                    };
                    eBuyDB.InStoreSales.Add(inStoreSale);
                    eBuyDB.SaveChanges();

                    foreach (var (product, quantity) in productsToSell)
                    {
                        var productExists = eBuyDB.Products.First(p => p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase));

                        productExists.Stock -= quantity;
                        ClsBranch.UpdateInventory(branchName, productExists.Name, productExists.Stock);

                        decimal itemTotal = productExists.SalePrice * quantity;
                        totalAmount += itemTotal;

                        InStoreSaleDetail detail = new InStoreSaleDetail
                        {
                            IdInStoreSale = inStoreSale.Id,
                            IdProduct = productExists.Id,
                            ProductName = productExists.Name,
                            Quantity = quantity,
                            UnitPrice = productExists.SalePrice,
                            TotalPrice = itemTotal
                        };
                        eBuyDB.InStoreSaleDetails.Add(detail);
                        eBuyDB.SaveChanges();

                        InStoreWarranty warranty = new InStoreWarranty
                        {
                            IdInStoreSaleDetail = detail.Id,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddMonths(productExists.WarrantyMonths),
                            WarrantyStatus = "Active"
                        };
                        eBuyDB.InStoreWarranties.Add(warranty);
                        eBuyDB.SaveChanges();
                    }
                    sale.TotalAmount = totalAmount;
                    eBuyDB.Entry(sale).State = EntityState.Modified;

                    InStoreSaleInvoice invoice = new InStoreSaleInvoice
                    {
                        IdInStoreSale = inStoreSale.Id,
                        InvoiceNumber = GenerateInvoiceNumber(),
                        TotalAmount = totalAmount,
                        PaymentMethod = paymentMethod,
                        BranchName = branch.Name,
                        BranchAddress = branch.Address
                    };
                    eBuyDB.InStoreSaleInvoices.Add(invoice);
                    eBuyDB.SaveChanges();

                    InStorePayment payment = new InStorePayment
                    {
                        IdInStoreSaleInvoice = invoice.Id,
                        PaymentDate = DateTime.Now,
                        Amount = totalAmount,
                        PaymentMethod = paymentMethod,
                        ReferenceNumber = GenerateReferenceNumber(),
                        PaymentStatus = "Completed"
                    };
                    eBuyDB.InStorePayments.Add(payment);
                    eBuyDB.SaveChanges();

                    transaction.Commit();

                    return "In store sale inserted successfully.";

                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }
    }
}