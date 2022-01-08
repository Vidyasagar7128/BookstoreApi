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
    public class ReviewsRepository : IReviewsRepository
    {
        public ReviewsRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Adding reviews
        /// </summary>
        /// <param name="reviews">passing reviews model</param>
        /// <returns>added or not</returns>
        public async Task<int> Add(ReviewsModel reviews)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {

                    using (SqlCommand sql = new SqlCommand("AddReviews", con))
                    {
                        sql.CommandType = System.Data.CommandType.StoredProcedure;
                        sql.Parameters.AddWithValue("@Rating", reviews.Rating);
                        sql.Parameters.AddWithValue("@Comment", reviews.Comment);
                        sql.Parameters.AddWithValue("@BookId", reviews.BookId);
                        sql.Parameters.AddWithValue("@UserId", reviews.UserId);
                        await con.OpenAsync();
                        int status = await sql.ExecuteNonQueryAsync();
                        await con.CloseAsync();
                        return status;
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
