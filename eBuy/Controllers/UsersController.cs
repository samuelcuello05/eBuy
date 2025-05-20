using eBuy.Clases;
using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        [HttpPost]
        [Route("Register")]
        public string CreateUser([FromBody] clsUserCustomerDTO dto)
        {
            clsUser user = new clsUser();
            return user.CreateUser(dto.User, dto.Customer);
        }
    }
}