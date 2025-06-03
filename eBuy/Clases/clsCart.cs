using eBuy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eBuy.Clases
{
    public class clsCart
    {
        private eBuyDBEntities eBuyDB = new eBuyDBEntities();
        public Cart cart { get; set; } = new Cart();

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

        public string AddItemToCart(int IdCart, int IdProduct, string branchName)
        {
            try
            {
                var cart = eBuyDB.Carts.Find(IdCart);
                if (cart == null)
                {
                    return "Error: Cart not found";
                }

                var existingBranch = eBuyDB.Branches.FirstOrDefault(b => b.Name.Equals(branchName, StringComparison.OrdinalIgnoreCase));
                if (existingBranch==null)
                {
                    return "Error: Branch not found.";
                }

                var existingProduct = eBuyDB.Products.FirstOrDefault(p => p.Id == IdProduct);
                if (existingProduct == null)
                {
                    return "Error: Product not found.";
                }

                var existingItem = eBuyDB.CartItems.FirstOrDefault(ci => ci.IdCart == IdCart && ci.IdProduct == IdProduct);
                if (existingItem != null)
                {
                    existingItem.Quantity += 1;
                }
                else
                {
                    CartItem newItem = new CartItem
                    {
                        IdCart = IdCart,
                        IdProduct = IdProduct,
                        Quantity = 1,
                        UnitPrice = eBuyDB.Products
                                        .Where(p => p.Id == IdProduct)
                                        .Select(p => p.SalePrice)
                                        .FirstOrDefault(),
                        BranchName = branchName
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

        public IEnumerable<object> GetCartProducts(int idCustomer)
        {
            try
            {
                var cart = eBuyDB.Carts.FirstOrDefault(c => c.IdCustomer == idCustomer);

                if (cart == null)
                {
                    throw new Exception("No cart found for the customer with ID: " + idCustomer);
                }

                var products = eBuyDB.CartItems
                    .Where(ci => ci.IdCart == cart.Id)
                    .Include(ci => ci.Product)
                    .Select(ci => new
                    {
                        ci.Product.Name,
                        ci.Quantity,
                        ci.UnitPrice,
                        ci.BranchName
                    })
                    .ToList();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving cart products: " + ex.Message);
            }
        }

    }
}