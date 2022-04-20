using CommonLayer.Models;
using CommonLayer.Models.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLyer.Interface
{
    public interface IBookRL
    {
        public AddBookModel AddBook(AddBookModel book);
        public bool DeleteBook(int bookId);
        public BookModel UpdateBook(BookModel book);
        public BookModel GetBookByBookId(int bookId);
        public List<BookModel> GetAllBooks();
    }
}
