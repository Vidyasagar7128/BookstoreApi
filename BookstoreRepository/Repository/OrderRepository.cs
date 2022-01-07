using BookstoreModel;
using BookstoreRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreRepository.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// order books
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <param name="bookId">passing bookId</param>
        /// <param name="addressId">passing addressId</param>
        /// <returns>ordered or not</returns>
        public async Task<int> Add(OrderModel order, int userId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("AddOrderSP", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@BookId", order.BookId);
                    sql.Parameters.AddWithValue("@Quantity", order.Quantity);
                    sql.Parameters.AddWithValue("@AddressId", order.AddressId);
                    sql.Parameters.AddWithValue("@UserId", userId);
                    await con.OpenAsync();
                    int status = await sql.ExecuteNonQueryAsync();
                    await con.CloseAsync();
                    return status;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Cancle order
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <param name="orderId">passing orderId</param>
        /// <returns>order cancle or not</returns>
        public async Task<int> Cancle(int userId, int orderId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("CancleOrderSP", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@OrderId", orderId);
                    sql.Parameters.AddWithValue("@UserId", userId);
                    await con.OpenAsync();
                    int status = await sql.ExecuteNonQueryAsync();
                    await con.CloseAsync();
                    return status;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
