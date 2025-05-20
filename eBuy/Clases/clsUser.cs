using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsUser
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public User user { get; set; }
        public clsCustomer customer { get; set; }

        public clsCart cart { get; set; }

        public string CreateUser(User userBD, Customer customerBD)
        {
            try
            {
                using (var transaction = eBuyDB.Database.BeginTransaction())
                {

                    if (eBuyDB.Users.Any(u => u.Email == userBD.Email))
                        return "Error: User already exists.";

                    if (customerBD == null)
                        return "Error: Customer data is missing.";

                    clsCypher cypher = new clsCypher();
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

                    cart.cart = new Cart();
                    string cartResult = cart.CreateCart(customerBD.Id);

                    if (cartResult.StartsWith("Error"))
                        throw new Exception(cartResult); // Forzar rollback si hay error en la creacion del carrito de compras para el usuario
                    transaction.Commit();


                    return "User successfully registered.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}