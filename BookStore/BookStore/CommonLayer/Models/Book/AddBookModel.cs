using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class AddBookModel
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public string BookDescription { get; set; }
        public float Rating { get; set; }
        public int Reviewer { get; set; }
        public string Image { get; set; }
        public int BookCount { get; set; }
    }
}
