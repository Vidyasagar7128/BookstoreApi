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
    }
}
