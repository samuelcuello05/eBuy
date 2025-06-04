using eBuy.Clases;
using eBuy.Clases.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/OnlineListingOwns")]
    public class OnlineListingOwnsController : ApiController
    {
        [HttpPost]
        [Route("Add")]
        [Authorize]
        public string AddOnlineListingOwn(int IdEmployee, string productName)
        {
            clsOnlineListingOwn onlineListingOwn = new clsOnlineListingOwn();

            return onlineListingOwn.AddOnlineListingOwn(IdEmployee, productName);
        }

        [HttpGet]
        [Route("List")]
        [Authorize]
        public IEnumerable<object> ListOnlineListingOwn()
        {
            clsOnlineListingOwn OnlineListingOwn = new clsOnlineListingOwn();

            return OnlineListingOwn.ListOnlineListingOwn();
        }
    }
}
