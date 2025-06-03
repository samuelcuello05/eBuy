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
        public string CreateCart([FromBody] Cart cart, int idCustomer)
        {
            clsCart Cart = new clsCart();
            Cart.cart = cart;
            return Cart.CreateCart(idCustomer);
        }

        [HttpPost]
        [Route("AddItem")]
        public string AddItemToCart(int idCart, int idProduct, string branchName)
        {
            clsCart Cart = new clsCart();
            return Cart.AddItemToCart(idCart, idProduct, branchName);
        }

        [HttpDelete]
        [Route("RemoveItem")]
        public string RemoveItemFromCart(int idCart, int idProduct)
        {
            clsCart Cart = new clsCart();
            return Cart.RemoveItemFromCart(idCart, idProduct);
        }

        [HttpDelete]
        [Route("Clear")]
        public string ClearCart(int idCart)
        {
            clsCart Cart = new clsCart();
            return Cart.ClearCart(idCart);
        }

        [HttpGet]
        [Route("ListCartProducts")]
        public IEnumerable<object> GetCartProducts(int idCustomer)
        {
            clsCart Cart = new clsCart();
            return Cart.GetCartProducts(idCustomer);
        }


    }
}