using CommonLayer.Models.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLyer.Interface
{
   public  interface ICartBL
   {
        public CartModel AddingToCart(CartModel cartModel, int userId);
        public Cart UpdateCartItems(Cart cart, int UserId);
        public bool DeleteCartItem(int CartID, int UserId);
        public List<CartModel> GetCartDetails(int UserId);
   }
}
