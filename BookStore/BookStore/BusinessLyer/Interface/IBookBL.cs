using CommonLayer.Models;
using CommonLayer.Models.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLyer.Interface
{
    public interface IBookBL
    {
        public AddBookModel AddBook(AddBookModel book);
        public bool DeleteBook(int bookId);
        public BookModel UpdateBook(BookModel book);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookByBookId(int bookId);
    }
}
