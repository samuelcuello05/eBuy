using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsSupplier
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();

        public string CreateSupplier(User userBD, Supplier supplierBD)
        {
            try
            {
                using (var transaction = eBuyDB.Database.BeginTransaction())
                {

                    if (userBD == null)
                        return "Error: User data is missing.";

                    if (supplierBD == null)
                        return "Error: Supplier data is missing.";

                    if (eBuyDB.Users.Any(u => u.Email == userBD.Email))
                        return "Error: User already exists.";

                    if(eBuyDB.Suppliers.Any(s => s.Phone == supplierBD.Phone))
                        return "Error: Supplier with this phone number already exists.";

                    if (eBuyDB.Suppliers.Any(s => s.Document == supplierBD.Document))
                        return "Error: Supplier with this document already exists.";

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
                    userBD.Role = "Supplier";
                    userBD.Status = true;

                    eBuyDB.Users.Add(userBD);
                    eBuyDB.SaveChanges();

                    supplierBD.IdUser = userBD.Id;
                    eBuyDB.Suppliers.Add(supplierBD);
                    eBuyDB.SaveChanges();

                    transaction.Commit();


                    return "Supplier successfully registered.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public List<Supplier> GetAllSuppliers()
        {
            try
            {
                return eBuyDB.Suppliers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving suppliers: " + ex.Message);
            }
        }

        public Supplier GetSupplierById(int id)
        {
            try
            {
                return eBuyDB.Suppliers.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving supplier: " + ex.Message);
            }
        }
    }
}