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

        [HttpPut]
        [Route("AddItem")]
        public string AddItemToCart(int idCart, int idProduct, int quantity, double unitPrice)
        {
            clsCart Cart = new clsCart();
            return Cart.AddItemToCart(idCart, idProduct, quantity, unitPrice);
        }

        [HttpPut]
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

    }
}