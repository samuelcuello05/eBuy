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
    [RoutePrefix("api/OnlineListingBySuppliers")]
    public class OnlineListingBySuppliersController : ApiController
    {
        [HttpPost]
        [Route("Add")]
        public string AddOnlineListingBySupplier(OnlineListing OnlineListing, int IdSupplier, string productName)
        {
            clsOnlineListingBySupplier onlineListingBySupplier = new clsOnlineListingBySupplier();

            return onlineListingBySupplier.AddOnlineListingBySupplier(OnlineListing,IdSupplier, productName);
        }

        [HttpGet]
        [Route("List")]
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
        [Route("Delete")]
        public string DeleteOnlineListingBySupplier(int IdOnlineListingBySupplier)
        {
            clsOnlineListingBySupplier onlineListingBySupplier = new clsOnlineListingBySupplier();

            return onlineListingBySupplier.DeleteOnlineListingBySupplier(IdOnlineListingBySupplier);
        }
    }
}

    
