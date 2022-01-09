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

        /// <summary>
        /// All reviews
        /// </summary>
        /// <param name="bookId">passing bookId</param>
        /// <returns>List of reviews</returns>
        public async Task<IEnumerable<ReviewsModel>> allReviews(int bookId)
        {
            try
            {
                List<ReviewsModel> reviews = new List<ReviewsModel>();
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    using (SqlCommand sql = new SqlCommand("select ReviewId, Rating, Comment, CreatedAt from Reviews where BookId='"+bookId+"'", con))
                    {
                        await con.OpenAsync();
                        SqlDataReader reader = await sql.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            ReviewsModel review = new ReviewsModel();
                            review.ReviewId = (int)reader["ReviewId"];
                            review.Rating = (double)reader["Rating"];
                            review.Comment = reader["Comment"].ToString();
                            review.CreatedAt = (DateTime)reader["CreatedAt"];
                            reviews.Add(review);
                        }
                        await con.CloseAsync();
                        return reviews;
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
