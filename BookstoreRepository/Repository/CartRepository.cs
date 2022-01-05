using BookstoreRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
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
        public async Task<int> Add(int userId, int bookId)
        {
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
            {
                SqlCommand sql = new SqlCommand("AddToCartSP", con);
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@BookId", bookId);
                sql.Parameters.AddWithValue("@UserId", userId);
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
        public async Task<int> Increase(int userId, int bookId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("CartIncreamentSP", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@BookId", bookId);
                    sql.Parameters.AddWithValue("@UserId", userId);
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
    }
}
