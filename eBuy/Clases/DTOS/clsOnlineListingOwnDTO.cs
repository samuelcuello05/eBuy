using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases.DTOS
{
    public class clsOnlineListingOwnDTO
    {
        public OnlineListing OnlineListing { get; set; }
        public Employee Employee { get; set; }
        public string ProductName { get; set; }
    }
}