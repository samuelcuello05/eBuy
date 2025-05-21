using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases.DTOS
{
    public class clsUserEmployeeDTO
    {
        public User User { get; set; }
        public Employee Employee { get; set; }
    }
}