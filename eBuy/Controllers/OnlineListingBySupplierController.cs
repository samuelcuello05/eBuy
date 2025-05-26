using eBuy.Clases;
using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eBuy.Clases.DTOS;

namespace eBuy.Controllers
{
    [RoutePrefix("api/OnlineListingBySupplier")]
    public class OnlineListingBySupplierController : ApiController
    {
        [HttpPost]
        [Route("Add")]
        public string AddOnlineListingBySupplier([FromBody] clsOnlineListingBySupplierDTO data)
        {
            clsOnlineListingBySupplier onlineListingBySupplier = new clsOnlineListingBySupplier();

            return onlineListingBySupplier.AddOnlineListingBySupplier(
                data.OnlineListing,
                data.Supplier,
                data.ProductName
            );
        }

        [HttpGet]
        [Route("List/{IdSupplier}")]
        public IEnumerable<object> ListOnlineListingBySupplier(int IdSupplier)
        {
            clsOnlineListingBySupplier onlineListingBySupplier = new clsOnlineListingBySupplier();

            return onlineListingBySupplier.ListOnlineListingBySupplier(IdSupplier);
        }
      
        [HttpPut]
        [Route("Update")]
        public string ModifyOnlineListingBySupplier([FromBody] OnlineListing updatedListing)
        {
            clsOnlineListingBySupplier onlineListingBySupplier = new clsOnlineListingBySupplier();

            return onlineListingBySupplier.ModifyOnlineListingBySupplier(updatedListing);
        }
     
        [HttpDelete]
        [Route("Delete/{id}")]
        public string DeleteOnlineListingBySupplier(int id)
        {
            clsOnlineListingBySupplier onlineListingBySupplier = new clsOnlineListingBySupplier();

            return onlineListingBySupplier.DeleteOnlineListingBySupplier(id);
        }
    }
}

    
