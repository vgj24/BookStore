using CommonLayer.Models;
using CommonLayer.Models.Book;
using Microsoft.Extensions.Configuration;
using RepoLyer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepoLyer.Services
{
    public class BookRL :IBookRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }

        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public AddBookModel AddBook(AddBookModel book)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookstoreDB"]);
                SqlCommand cmd = new SqlCommand("SPAddBooks", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookName", book.BookName);
                cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
                cmd.Parameters.AddWithValue("@BookDescription", book.BookDescription);
                cmd.Parameters.AddWithValue("@Rating", book.Rating);
                cmd.Parameters.AddWithValue("@Image", book.Image);
                cmd.Parameters.AddWithValue("@Reviewer", book.Reviewer);
                cmd.Parameters.AddWithValue("@BookCount", book.BookCount);

                this.sqlConnection.Open();
                int check = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (check >= 1)
                {
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
       
        public bool DeleteBook(int bookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookstoreDB"]);
                SqlCommand cmd = new SqlCommand("spDeleteBooks", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookId", bookId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public BookModel UpdateBook(BookModel book)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookstoreDB"]);
                SqlCommand cmd = new SqlCommand("sp_UpdateBooks", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookId",book.BookId);
                cmd.Parameters.AddWithValue("@BookName", book.BookName);
                cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("@OriginalPrice", book.OriginalPrice);
                cmd.Parameters.AddWithValue("@BookDescription", book.BookDescription);
                cmd.Parameters.AddWithValue("@Rating", book.Rating);
                cmd.Parameters.AddWithValue("@Image", book.Image);
                cmd.Parameters.AddWithValue("@Reviewer", book.Reviewer);
                cmd.Parameters.AddWithValue("@BookCount", book.BookCount);
                this.sqlConnection.Open();
                int check = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (check >= 1)
                {
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookstoreDB"]);
                SqlCommand cmd = new SqlCommand("SPGetBooksById", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookId", bookId);
                this.sqlConnection.Open();
                BookModel bookModel = new BookModel();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bookModel.BookId = Convert.ToInt32(reader["BookId"]);
                        bookModel.BookName = reader["BookName"].ToString();
                        bookModel.AuthorName = reader["AuthorName"].ToString();
                        bookModel.DiscountPrice = Convert.ToDecimal(reader["DiscountPrice"]);
                        bookModel.OriginalPrice = Convert.ToDecimal(reader["OriginalPrice"]);
                        bookModel.Rating = Convert.ToInt32(reader["Rating"]);
                        bookModel.BookDescription = reader["BookDescription"].ToString();
                        bookModel.Reviewer =Convert.ToInt32(reader["Reviewer"].ToString());
                        bookModel.Image = reader["Image"].ToString();
                        bookModel.BookCount = Convert.ToInt32(reader["BookCount"]);
                    }

                    this.sqlConnection.Close();
                    return bookModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
        public List<BookModel> GetAllBooks()
        {
            try
            {
                List<BookModel> book = new List<BookModel>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookstoreDB"]);
                SqlCommand cmd = new SqlCommand("SPGetAllBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Add(new BookModel
                        {
                            BookId = Convert.ToInt32(reader["BookId"]),
                            BookName = reader["BookName"].ToString(),
                            AuthorName = reader["AuthorName"].ToString(),
                            Rating = Convert.ToInt32(reader["Rating"]),
                            Reviewer = Convert.ToInt32(reader["Reviewer"]),
                            DiscountPrice = Convert.ToDecimal(reader["DiscountPrice"]),
                            OriginalPrice = Convert.ToDecimal(reader["OriginalPrice"]),
                            BookDescription = reader["BookDescription"].ToString(),
                            Image = reader["Image"].ToString(),
                            BookCount = Convert.ToInt32(reader["BookCount"])
                        });
                    }

                    this.sqlConnection.Close();
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

    }
}

