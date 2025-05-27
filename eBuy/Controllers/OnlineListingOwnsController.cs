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
        public string AddOnlineListingOwn([FromBody] clsOnlineListingOwnDTO data)
        {
            clsOnlineListingOwn onlineListingOwn = new clsOnlineListingOwn();

            return onlineListingOwn.AddOnlineListingOwn(
                data.OnlineListing,
                data.Employee,
                data.ProductName
            );
        }
    }
}
