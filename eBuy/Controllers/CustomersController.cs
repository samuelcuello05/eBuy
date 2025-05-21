using eBuy.Clases;
using eBuy.Clases.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/Customers")]
    public class CustomersController : ApiController
    {
        [HttpPost]
        [Route("Register")]
        public string CreateCustomer([FromBody] clsUserCustomerDTO dto)
        {
            clsCustomer customer = new clsCustomer();
            return customer.CreateCustomer(dto.User, dto.Customer);
        }
    }
}