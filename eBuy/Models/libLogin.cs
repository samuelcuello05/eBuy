using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Models
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Page { get; set; }
    }
    public class LoginRespuesta
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string UserType { get; set; }
        public string StartPage { get; set; }
        public bool Auth { get; set; }
        public bool Status { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}