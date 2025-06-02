using eBuy.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/OnlineListings")]
    public class OnlineListingsController : ApiController
    {
        [HttpGet]
        [Route("List")]
        public IEnumerable<object> ListOnlineListings()
        {
            try
            {
                clsOnlineListing onlineListing = new clsOnlineListing();
                return onlineListing.ListOnlineListing();
            }
            catch (Exception ex)
            {
                return new List<object> { $"Error: {ex.Message}" };
            }
        }
    }
}