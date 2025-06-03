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
    [RoutePrefix("api/Suppliers")]
    public class SuppliersController : ApiController
    {
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public string CreateSupplier([FromBody] clsUserSupplierDTO dto)
        {
            clsSupplier supplier = new clsSupplier();
            return supplier.CreateSupplier(dto.User, dto.Supplier);
        }

        [HttpGet]
        [Route("List")]
        [Authorize]
        public List<Supplier> GetAllSuppliers()
        {
            clsSupplier supplier = new clsSupplier();
            return supplier.GetAllSuppliers();
        }

        [HttpGet]
        [Route("GetById")]
        [Authorize]
        public Supplier GetSupplierById(int IdSupplier)
        {
            clsSupplier supplier = new clsSupplier();
            return supplier.GetSupplierById(IdSupplier);
        }
    }
    
}