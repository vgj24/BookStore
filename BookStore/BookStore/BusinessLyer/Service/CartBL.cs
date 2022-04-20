using BusinessLyer.Interface;
using CommonLayer.Models.Cart;
using RepoLyer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLyer.Service
{
    public class CartBL :ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        public CartModel AddingToCart(CartModel cartModel,int userId)
        {
            try
            {
                return this.cartRL.AddingToCart(cartModel,userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Cart UpdateCartItems(Cart cart, int UserId)
        {
            try
            {
                return this.cartRL.UpdateCartItems(cart,UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteCartItem(int CartID, int UserId)
        {
            try
            {
                return this.DeleteCartItem(CartID,UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<CartModel> GetCartDetails(int UserId)
        {
            try
            {
                return this.cartRL.GetCartDetails(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
