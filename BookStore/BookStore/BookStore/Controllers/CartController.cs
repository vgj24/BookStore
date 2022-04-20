using BusinessLyer.Interface;
using CommonLayer.Models.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        
        [HttpPost("Add")]
        public IActionResult AddCart(CartModel cartModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.AddingToCart(cartModel,userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Book Added in Cart ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Cart Add failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [HttpPut("Update")]
        public IActionResult UpdateCart(Cart cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.UpdateCartItems(cart, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Book Updated in Cart ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "cart Update failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.cartBL.DeleteCartItem(cartId, userId))
                {
                    return this.Ok(new { success = true, message = "Book Deleted from Cart " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "cart Delete  failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [HttpGet("{UserId}/ Get")]
        public IActionResult GetCart()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.GetCartDetails(userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Cat Data Fetched Successfully ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User Id is Wrong" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
    }

}       

    

