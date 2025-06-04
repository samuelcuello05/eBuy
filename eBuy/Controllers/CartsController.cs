using eBuy.Clases;
using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eBuy.Controllers
{
    [RoutePrefix("api/Carts")]
    public class CartsController : ApiController
    {
        [HttpPost]
        [Route("Create")]
        [Authorize]
        public string CreateCart([FromBody] Cart cart, int idCustomer)
        {
            clsCart Cart = new clsCart();
            Cart.cart = cart;
            return Cart.CreateCart(idCustomer);
        }

        [HttpPost]
        [Route("AddItem")]
        [Authorize]
        public string AddItemToCart(int idCustomer, int idProduct, string branchName)
        {
            clsCart Cart = new clsCart();
            return Cart.AddItemToCart(idCustomer, idProduct, branchName);
        }

        [HttpDelete]
        [Route("RemoveItem")]
        [Authorize]
        public string RemoveItemFromCart(int idCustomer, int idProduct)
        {
            clsCart Cart = new clsCart();
            return Cart.RemoveItemFromCart(idCustomer, idProduct);
        }

        [HttpDelete]
        [Route("Clear")]
        [Authorize]
        public string ClearCart(int idCustomer)
        {
            clsCart Cart = new clsCart();
            return Cart.ClearCart(idCustomer);
        }

        [HttpGet]
        [Route("ListCartProducts")]
        [Authorize]
        public IEnumerable<object> GetCartProducts(int idCustomer)
        {
            clsCart Cart = new clsCart();
            return Cart.GetCartProducts(idCustomer);
        }


    }
}