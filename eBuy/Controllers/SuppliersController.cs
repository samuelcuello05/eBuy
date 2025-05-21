using eBuy.Clases;
using eBuy.Clases.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/Suppliers")]
    public class SuppliersController : ApiController
    {
        [HttpPost]
        [Route("Register")]
        public string CreateSupplier([FromBody] clsUserSupplierDTO dto)
        {
            clsSupplier supplier = new clsSupplier();
            return supplier.CreateSupplier(dto.User, dto.Supplier);
        }
    }
    
}