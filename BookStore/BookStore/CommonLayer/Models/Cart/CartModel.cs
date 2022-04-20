using CommonLayer.Models.Book;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models.Cart
{
   public class CartModel
    {
        public int CartID { get; set; }
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }
        public int OrderQuantity { get; set; }
        //public BookModel Bookmodel { get; set; }
    }
}
