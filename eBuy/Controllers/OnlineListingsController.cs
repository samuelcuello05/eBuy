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
        [AllowAnonymous]
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
        [Authorize]
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
        [AllowAnonymous]

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

        [HttpPut]
        [Route("ActivateAndDeactivate")]
        [Authorize]
        public IHttpActionResult ActivateAndDeactivateOnlineListing(int IdOnlineListing)
        {
            try
            {
                clsOnlineListing onlineListing = new clsOnlineListing();
                var result = onlineListing.ActivateAndDeactivateOnlineListing(IdOnlineListing);
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

        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public IHttpActionResult DeleteOnlineListing(int IdOnlineListing)
        {
            try
            {
                clsOnlineListing onlineListing = new clsOnlineListing();
                var result = onlineListing.DeleteOnlineListing(IdOnlineListing);
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