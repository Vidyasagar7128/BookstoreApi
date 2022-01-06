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
    public class AddressRepository : IAddressRepository
    {
        public AddressRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Adding address to user
        /// </summary>
        /// <param name="address">passing AddressModel</param>
        /// <returns>added or not</returns>
        public async Task<int> AddNew(AddressModel address)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("SetAddressSP", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@Address", address.Address);
                    sql.Parameters.AddWithValue("@City", address.City);
                    sql.Parameters.AddWithValue("@State", address.State);
                    sql.Parameters.AddWithValue("@Type", address.Type);
                    sql.Parameters.AddWithValue("@UserId", address.UserId);
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
        /// Updating address
        /// </summary>
        /// <param name="address">passing AddressModel</param>
        /// <returns>updated or not</returns>
        public async Task<int> Update(AddressModel address)
        {
            using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
            {
                SqlCommand sql1 = new SqlCommand("SELECT AddressId, UserId FROM Address WHERE AddressId='" + address.AddressId + "' AND UserId='"+ address.UserId +"'", con);
                await con.OpenAsync();
                SqlDataReader reader = await sql1.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    await con.CloseAsync();
                    SqlCommand sql = new SqlCommand("UpdateAddressSP", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@AddressId", address.AddressId);
                    sql.Parameters.AddWithValue("@Address", address.Address);
                    sql.Parameters.AddWithValue("@City", address.City);
                    sql.Parameters.AddWithValue("@State", address.State);
                    sql.Parameters.AddWithValue("@Type", address.Type);
                    sql.Parameters.AddWithValue("@UserId", address.UserId);
                    await con.OpenAsync();
                    int status = await sql.ExecuteNonQueryAsync();
                    await con.CloseAsync();
                    return status;
                }
                else
                {
                    await con.CloseAsync();
                    return 0;
                }
            }
        }

        /// <summary>
        /// delete address
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <param name="addressId">passing addressId</param>
        /// <returns>deleted or not</returns>
        public async Task<int> Delete(int userId, int addressId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("DELETE FROM Address WHERE AddressId='" +addressId+ "' AND UserId='"+userId+"'", con);
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
