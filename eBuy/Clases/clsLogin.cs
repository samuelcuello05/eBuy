using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace eBuy.Clases
{
    public class clsLogin
    {
        public clsLogin()
        {
            loginRespuesta = new LoginRespuesta();
        }

        public eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public Models.Login login { get; set; }
        public LoginRespuesta loginRespuesta { get; set; }

        public bool ValidarUsuario()
        {
            try
            {
                clsCypher cifrar = new clsCypher();
                User user = eBuyDB.Users.FirstOrDefault(u => u.Email == login.Email);
                if (user == null)
                {
                    loginRespuesta.Auth = false;
                    loginRespuesta.Message = "User not exists";
                    return false;
                }
                byte[] arrBytesSalt = Convert.FromBase64String(user.Salt);
                string ClaveCifrada = cifrar.HashPassword(login.Password, arrBytesSalt);
                login.Password = ClaveCifrada;
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Auth = false;
                loginRespuesta.Message = ex.Message;
                return false;
            }
        }
        private bool ValidarClave()
        {
            try
            {
                User usuario = eBuyDB.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
                if (usuario == null)
                {
                    loginRespuesta.Auth = false;
                    loginRespuesta.Message = "Invalid password.";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Auth = false;
                loginRespuesta.Message = ex.Message;
                return false;
            }
        }
        public LoginRespuesta Ingresar()
        {
            if (ValidarUsuario() && ValidarClave())
            {
                User usuario = eBuyDB.Users.FirstOrDefault(u => u.Email == login.Email);

                if (usuario == null)
                {
                    loginRespuesta.Auth = false;
                    loginRespuesta.Message = "User not found after validation.";
                    return loginRespuesta;
                }

                // Identificar tipo de usuario
                string userType = "";
                if (eBuyDB.Customers.Any(c => c.IdUser == usuario.Id))
                {
                    userType = "Customer";
                }
                else if (eBuyDB.Employees.Any(e => e.IdUser == usuario.Id))
                {
                    userType = "Employee";
                }
                else if (eBuyDB.Suppliers.Any(s => s.IdUser == usuario.Id))
                {
                    userType = "Supplier";
                }
                else
                {
                    loginRespuesta.Auth = false;
                    loginRespuesta.Message = "User type not recognized.";
                    return loginRespuesta;
                }

                string startPage = "";
                switch (userType)
                {
                    case "Customer":
                        startPage = "/customer/home";
                        break;
                    case "Employee":
                        startPage = "/employee/dashboard";
                        break;
                    case "Supplier":
                        startPage = "/supplier/overview";
                        break;
                }

                // Generar token
                string token = TokenGenerator.GenerateTokenJwt(usuario.Email);

                // Armar respuesta
                loginRespuesta.Auth = true;
                loginRespuesta.Message = "Login successful.";
                loginRespuesta.IdUser = usuario.Id;
                loginRespuesta.Email = usuario.Email;
                loginRespuesta.Role = usuario.Role;
                loginRespuesta.Status = usuario.Status;
                loginRespuesta.UserType = userType;
                loginRespuesta.Token = token;
                loginRespuesta.StartPage = startPage;


                return loginRespuesta;
            }
            else
            {
                return loginRespuesta;
            }
        }

    }
}