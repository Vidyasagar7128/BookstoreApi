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
    public class FavoriteRepository : IFavoriteRepository
    {
        public FavoriteRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Adding to favorite
        /// </summary>
        /// <param name="favorite">passing favorite</param>
        /// <returns>added or not</returns>
        public async Task<int> Add(int userId, int bookId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("AddToWishlistSP", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@BookId", bookId);
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
        /// removing from favorite
        /// </summary>
        /// <param name="favorite">passing favorite</param>
        /// <returns>added or not</returns>
        public async Task<int> Remove(int userId, int bookId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("RemoveFromWishlistSP", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@BookId", bookId);
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
        /// All favorite list
        /// </summary>
        /// <param name="userId">passing UserId</param>
        /// <returns> All favorite list</returns>
        public async Task<IEnumerable<BookModel>> all(int userId)
        {
            try
            {
                List<BookModel> books = new List<BookModel>();
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("DataFromWishlistSP", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@UserId",userId);
                    await con.OpenAsync();
                    SqlDataReader reader = await sql.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        BookModel bookModel = new BookModel();
                        bookModel.BookId = (int)reader["BookId"];
                        bookModel.Title = reader["Title"].ToString();
                        bookModel.Author = reader["Author"].ToString();
                        bookModel.Rating = (double)reader["Rating"];
                        bookModel.Reviews = reader["Reviews"] == null ? 0 : (int)reader["Reviews"];
                        bookModel.Quantity = reader["Quantity"] == null ? 0 : (int)reader["Quantity"];
                        bookModel.Price = (double)reader["Price"];
                        bookModel.Details = reader["Details"].ToString();
                        books.Add(bookModel);
                    }
                    await con.CloseAsync();
                    return books;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
