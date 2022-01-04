﻿using BookstoreModel;
using BookstoreRepository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreRepository.Repository
{
    public class BookRepository : IBookRepository
    {
        public BookRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// AllBooks
        /// </summary>
        /// <returns>List of Data</returns>
        public async Task<IEnumerable<BookModel>> AllBooks()
        {
            try
            {
                List<BookModel> books = new List<BookModel>();
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("select * from Books", con);
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
                }
                return books;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Adding Books
        /// </summary>
        /// <param name="bookModel">object of bookmodel</param>
        /// <returns>added or not</returns>
        public async Task<int> AddBook(BookModel bookModel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("AddBookSP", con);
                    sql.CommandType = System.Data.CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@Title", bookModel.Title);
                    sql.Parameters.AddWithValue("@Author", bookModel.Author);
                    sql.Parameters.AddWithValue("@Rating", bookModel.Rating);
                    sql.Parameters.AddWithValue("@Reviews", bookModel.Reviews);
                    sql.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    sql.Parameters.AddWithValue("@Price", bookModel.Price);
                    sql.Parameters.AddWithValue("@Details", bookModel.Details);
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

        public async Task<int> Delete(int bookId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.Configuration.GetConnectionString("database")))
                {
                    SqlCommand sql = new SqlCommand("delete from Books where BookId = '" + bookId + "'", con);
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
