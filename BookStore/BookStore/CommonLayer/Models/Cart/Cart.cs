using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models.Cart
{
    public class Cart
    {
        public int CartID { get; set; }
        public int BookId { get; set; }

        public int OrderQuantity { get; set; }
    }
}
