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

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult GetOnlineListingById(int IdOnlineListing)
        {
            try
            {
                clsOnlineListing onlineListing = new clsOnlineListing();
                var result = onlineListing.GetOnlineListingById(IdOnlineListing);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("GetOnlineListingPublisherName")]
        public IHttpActionResult GetOnlineListingPublisherName(int IdOnlineListing)
        {
            try
            {
                clsOnlineListing onlineListing = new clsOnlineListing();
                var result = onlineListing.GetOnlineListingPublisherName(IdOnlineListing);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}