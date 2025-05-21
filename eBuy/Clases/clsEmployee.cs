using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsEmployee
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public Employee employee { get; set; }

        public string CreateEmployee(User userBD, Employee employeeBD, string branchName)
        {
            try
            {
                using (var transaction = eBuyDB.Database.BeginTransaction())
                {

                    if (userBD == null)
                        return "Error: User data is missing.";

                    if (employeeBD == null)
                        return "Error: Employee data is missing.";

                    var branch = eBuyDB.Branches.FirstOrDefault(b => b.Name.Equals(branchName, StringComparison.OrdinalIgnoreCase));
                    if (branch == null)
                    {
                        return "Error: Branch not found";
                    }

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
                    userBD.Role = "Employee";
                    userBD.Status = true;

                    eBuyDB.Users.Add(userBD);
                    eBuyDB.SaveChanges();

                    employeeBD.IdUser = userBD.Id;
                    employeeBD.IdBranch = branch.Id;
                    eBuyDB.Employees.Add(employeeBD);
                    eBuyDB.SaveChanges();

                    transaction.Commit();


                    return "Employee successfully registered.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}