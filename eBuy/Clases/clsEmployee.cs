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
                    cypher.Password = userBD.Password;
                    if (cypher.CifrarClave())
                    {
                        string EncryptedPassword = cypher.PasswordCifrado;

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
                    employeeBD.AssignedBranch = branchName;
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

        public List<Employee> GetAllEmployees()
        {
            try
            {
                return eBuyDB.Employees.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving employees: " + ex.Message);
            }
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                return eBuyDB.Employees.FirstOrDefault(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving employee: " + ex.Message);
            }
        }
    }
}