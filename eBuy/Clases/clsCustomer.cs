﻿using eBuy.Models;
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


                    return "Customer successfully registered.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}