using CommonLayer.Models.Book;
using CommonLayer.Models.Cart;
using Microsoft.Extensions.Configuration;
using RepoLyer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepoLyer.Services
{
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        public CartRL(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public CartModel AddingToCart(CartModel cartModel, int userId)
        {

            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookstoreDB"]);
                SqlCommand cmdtype = new SqlCommand("SPAddToCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmdtype.Parameters.AddWithValue("@UserId", cartModel.UserId);
                cmdtype.Parameters.AddWithValue("@BookId", cartModel.BookId);
                cmdtype.Parameters.AddWithValue("@OrderQuantity", cartModel.OrderQuantity);


                this.sqlConnection.Open();
                int i = cmdtype.ExecuteNonQuery();
                this.sqlConnection.Close();

                if (i >= 1)
                {
                    return cartModel;
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
        public Cart UpdateCartItems(Cart cart, int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookstoreDB"]);
                SqlCommand cmdtype = new SqlCommand("UpdateCartItems", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmdtype.Parameters.AddWithValue("@UserId", UserId);
                cmdtype.Parameters.AddWithValue("@BookId", cart.BookId);
                cmdtype.Parameters.AddWithValue("@OrderQuantity", cart.OrderQuantity);
               cmdtype.Parameters.AddWithValue("@CartID",cart.CartID);
                this.sqlConnection.Open();
                int i = cmdtype.ExecuteNonQuery();
                this.sqlConnection.Close();

                if (i >= 1)
                {
                    return cart;
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
        public bool DeleteCartItem(int CartID, int UserId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStoreDB"]);
                SqlCommand cmd = new SqlCommand("DeleteCartItem", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CartID", CartID);
                cmd.Parameters.AddWithValue("@UserId", UserId);
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

        public List<CartModel> GetCartDetails(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionStrings:BookStoreDB"]);
                SqlCommand cmd = new SqlCommand("GetCartbyUser", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<CartModel> cartmodel = new List<CartModel>();
                    while (reader.Read())
                    {
                        BookModel booksModel = new BookModel();
                        CartModel cart = new CartModel();

                        booksModel.BookName = reader["BookName"].ToString();
                        booksModel.AuthorName = reader["AuthorName"].ToString();
                        booksModel.OriginalPrice = Convert.ToDecimal(reader["OriginalPrice"]);
                        booksModel.DiscountPrice = Convert.ToDecimal(reader["DiscountPrice"]);
                        booksModel.Image = reader["BookImage"].ToString();
                        cart.UserId = Convert.ToInt32(reader["UserId"]);
                        cart.BookId = Convert.ToInt32(reader["BookId"]);
                        cart.CartID = Convert.ToInt32(reader["CartID"]);
                        cart.OrderQuantity = Convert.ToInt32(reader["OrderQuantity"]);
                       // cart.Bookmodel = booksModel;
                        cartmodel.Add(cart);
                    }

                    this.sqlConnection.Close();
                    return cartmodel;
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

