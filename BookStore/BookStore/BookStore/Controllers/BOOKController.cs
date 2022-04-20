using BusinessLyer.Interface;
using CommonLayer.Models;
using CommonLayer.Models.Book;
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
    public class BOOKController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BOOKController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

       
        [HttpPost("post")]
        public IActionResult AddBook(AddBookModel book)
        {
            try
            {
                var bookDetail = this.bookBL.AddBook(book);
                if (bookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Added Sucessfully", Response = bookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpPut("Update")]
        public IActionResult UpdateBook(BookModel book)
        {
            try
            {
                var updatedBookDetail = this.bookBL.UpdateBook(book); ;
                if (updatedBookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Updated Sucessfully", Response = updatedBookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
       
        [HttpDelete("Delete")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                if (this.bookBL.DeleteBook(bookId))
                {
                    return this.Ok(new { Success = true, message = "Book Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpGet("{bookId}/Get")]
        public IActionResult GetBookByBookId(int bookId)
        {
            try
            {
                var updatedBookDetail = this.bookBL.GetBookByBookId(bookId);
                if (updatedBookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Detail Fetched Sucessfully", Response = updatedBookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpGet("Get")]
        public IActionResult GetBook()
        {
            try
            {
                var updatedBookDetail = this.bookBL.GetAllBooks();
                if (updatedBookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Detail Fetched Sucessfully", Response = updatedBookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
