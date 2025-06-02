using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsCustomer
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public clsCart cart { get; set; }

        public string CreateCustomer(User userBD, Customer customerBD)
        {
            try
            {
                using (var transaction = eBuyDB.Database.BeginTransaction())
                {

                    if (userBD == null)
                        return "Error: User data is missing.";

                    if (customerBD == null)
                        return "Error: Customer data is missing.";

                    if (eBuyDB.Users.Any(u => u.Email == userBD.Email))
                        return "Error: User already exists.";

                    if (eBuyDB.Customers.Any(c => c.Document == customerBD.Document))
                        return "Error: Customer with this document already exists.";

                    clsCypher cypher = new clsCypher();
                    cypher.Password = userBD.Password;
                    string EncryptedPassword;

                    if (cypher.CifrarClave())
                    {
                        EncryptedPassword = cypher.PasswordCifrado;
                    }
                    else
                    {
                        return "Error: Password could not be encrypted.";
                    }

                    userBD.Password = cypher.PasswordCifrado;
                    userBD.Salt = cypher.Salt;
                    userBD.Role = "Customer";
                    userBD.Status = true;

                    eBuyDB.Users.Add(userBD);
                    eBuyDB.SaveChanges();

                    customerBD.IdUser = userBD.Id;
                    eBuyDB.Customers.Add(customerBD);
                    eBuyDB.SaveChanges();

                    Cart cart = new Cart
                    {
                        IdCustomer = customerBD.Id,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };
                    eBuyDB.Carts.Add(cart);
                    eBuyDB.SaveChanges();

                    transaction.Commit();

                    return "Customer successfully registered.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public List<Customer> GetAllCustomers()
        {
            try
            {
                return eBuyDB.Customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customers: " + ex.Message);
            }
        }

        public Customer GetCustomerById(int id)
        {
            try
            {
                return eBuyDB.Customers.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customer: " + ex.Message);
            }
        }
    }
}