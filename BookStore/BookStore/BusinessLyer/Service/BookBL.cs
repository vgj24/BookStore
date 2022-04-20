using BusinessLyer.Interface;
using CommonLayer.Models;
using CommonLayer.Models.Book;
using RepoLyer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLyer.Service
{
    public class BookBL :IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public AddBookModel AddBook(AddBookModel book)
        {
            try
            {
                return this.bookRL.AddBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteBook(int bookId)
        {
            try
            {
                return this.bookRL.DeleteBook(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public BookModel UpdateBook(BookModel book)
        {
            try
            {
                return this.bookRL.UpdateBook(book); ;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                return this.bookRL.GetBookByBookId(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }
       
    }
}

