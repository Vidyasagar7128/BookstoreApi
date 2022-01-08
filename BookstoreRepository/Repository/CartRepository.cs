using BookstoreModel;
using BookstoreRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BookstoreRepository.Repository
{
    public class CartRepository : ICartRepository
    {
        public CartRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Add to cart
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <param name="bookId">passing bookId</param>
        /// <returns>Added or not</returns>
        public async Task<int> Add(int userId, int bookId, int price)
        {
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
            {
                SqlCommand sql = new SqlCommand("AddToCartSP", con);
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@BookId", bookId);
                sql.Parameters.AddWithValue("@UserId", userId);
                sql.Parameters.AddWithValue("@Price", price);
                await con.OpenAsync();
                int status = await sql.ExecuteNonQueryAsync();
                await con.CloseAsync();
                return status;
            }
        }

        /// <summary>
        /// remove book from cart
        /// </summary>
        /// <param name="bookId">passing bookId</param>
        /// <param name="userId">passing userId</param>
        /// <returns>deleted or not</returns>
        public async Task<int> Remove(int userId, int bookId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("delete from Cart where BookId='" + bookId + "'AND UserId='" + userId + "'", con);
                    await con.OpenAsync();
                    int result = await sql.ExecuteNonQueryAsync();
                    await con.CloseAsync();
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Increase Quantity
        /// </summary>
        /// <param name="userId">Passing userId</param>
        /// <param name="bookId">Passing bookId</param>
        /// <returns>Increased or not</returns>
        public async Task<int> Increase(QuantityModel quantity)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("CartIncreamentSP", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@CartId", quantity.CartId);
                    sql.Parameters.AddWithValue("@Quantity", quantity.Quantity);
                    sql.Parameters.AddWithValue("@Price", quantity.Quantity*quantity.Price);
                    sql.Parameters.AddWithValue("@UserId", quantity.UserId);
                    await con.OpenAsync();
                    int result = await sql.ExecuteNonQueryAsync();
                    await con.CloseAsync();
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// get all cart items
        /// </summary>
        /// <param name="userId">form token</param>
        /// <returns>list of cart item</returns>
        public async Task<IEnumerable<CartModel>> Items(int userId)
        {
            try
            {
                List<CartModel> cart = new List<CartModel>();
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("ShowCartItems", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@UserId",userId);
                    await con.OpenAsync();
                    SqlDataReader reader = await sql.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        CartModel cartModel = new CartModel();
                        cartModel.CartId = (int)reader["CartId"];
                        cartModel.BookId = (int)reader["BookId"];
                        cartModel.Quantity = (int) reader["Quantity"];
                        cartModel.Title = reader["Title"].ToString();
                        cartModel.Author = reader["Author"].ToString();
                        cartModel.Rating = (double)reader["Rating"];
                        cartModel.Reviews = (int)reader["Reviews"];
                        cartModel.Price = (double)reader["Price"];
                        cartModel.Details = reader["Details"].ToString();
                        cart.Add(cartModel);
                    }
                    await con.CloseAsync();
                }
                return cart;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
