using eBuy.Clases;
using eBuy.Clases.DTOS;
using eBuy.Models;
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
        [AllowAnonymous]
        public string CreateCustomer([FromBody] clsUserCustomerDTO dto)
        {
            clsCustomer customer = new clsCustomer();
            return customer.CreateCustomer(dto.User, dto.Customer);
        }

        [HttpGet]
        [Route("List")]
        [Authorize]
        public List<Customer> GetAllCustomers()
        {
            clsCustomer customer = new clsCustomer();
            return customer.GetAllCustomers();
        }

        [HttpGet]
        [Route("GetById")]
        [Authorize]
        public Customer GetCustomerById(int IdCustomer)
        {
            clsCustomer customer = new clsCustomer();
            return customer.GetCustomerById(IdCustomer);
        }
    }
}