using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsCart
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public Cart cart { get; set; } = new Cart();

        public CartItem cartItem { get; set; }

        public string CreateCart(int IdCustomer)
        {
            try
            {
                cart.IdCustomer = IdCustomer;
                cart.CreatedAt = DateTime.Now;
                cart.UpdatedAt = DateTime.Now;
                eBuyDB.Carts.Add(cart);
                eBuyDB.SaveChanges();
                return "Cart created successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string AddItemToCart(int IdCart, int IdProduct, int quantity, double unitPrice)
        {
            try
            {
                var cart = eBuyDB.Carts.Find(IdCart);
                if (cart == null)
                {
                    return "Error: Cart not found";
                }

                var existingItem = eBuyDB.CartItems.FirstOrDefault(ci => ci.IdCart == IdCart && ci.IdProduct == IdProduct);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    CartItem newItem = new CartItem
                    {
                        IdCart = IdCart,
                        IdProduct = IdProduct,
                        Quantity = quantity,
                        UnitPrice = (decimal)unitPrice
                    };
                    eBuyDB.CartItems.Add(newItem);
                }


                cart.UpdatedAt = DateTime.Now;
                eBuyDB.SaveChanges();

                return "Item added to cart successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string RemoveItemFromCart(int IdCart, int IdProduct)
        {
            try
            {
                var cart = eBuyDB.Carts.Find(IdCart);
                if (cart == null)
                {
                    return "Error: Cart not found";
                }

                var itemToRemove = eBuyDB.CartItems.FirstOrDefault(ci => ci.IdCart == IdCart && ci.IdProduct == IdProduct);
                if (itemToRemove != null)
                {
                    if (itemToRemove.Quantity > 1)
                    {
                        itemToRemove.Quantity--;
                    }
                    else
                    {
                        eBuyDB.CartItems.Remove(itemToRemove);
                    }
                    cart.UpdatedAt = DateTime.Now;
                    eBuyDB.SaveChanges();
                    return "Item removed from cart successfully";
                }
                else
                {
                    return "Error: Item not found in cart";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string ClearCart(int IdCart)
        {
            try
            {
                var cart = eBuyDB.Carts.Find(IdCart);
                if (cart == null)
                {
                    return "Error: Cart not found";
                }
                var itemsToRemove = eBuyDB.CartItems.Where(ci => ci.IdCart == IdCart).ToList();
                foreach (var item in itemsToRemove)
                {
                    eBuyDB.CartItems.Remove(item);
                }
                cart.UpdatedAt = DateTime.Now;
                eBuyDB.SaveChanges();
                return "Cart cleared successfully";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}