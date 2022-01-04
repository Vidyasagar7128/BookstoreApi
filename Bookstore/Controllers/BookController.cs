﻿using BookstoreManager.Interfaces;
using BookstoreModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookManager _bookManager;
        public BookController(IBookManager bookManager)
        {
            this._bookManager = bookManager;
        }

        /// <summary>
        /// Getting All books
        /// </summary>
        /// <returns>List of Books</returns>
        [HttpGet]
        [Route("Books")]
        public async Task<IActionResult> ShowBooks()
        {
            try
            {
                IEnumerable<BookModel> result = await this._bookManager.Books();
                if (result.Count() >= 1)
                {
                    return this.Ok(new { status = true, Response = result });
                }
                else
                {
                    return this.BadRequest(new { status = false, Response = result });
                }
            }
            catch(Exception e)
            {
                return this.NotFound(e.Message);
            }
        }

        /// <summary>
        /// BookAdd
        /// </summary>
        /// <param name="bookModel">BookModel from body</param>
        /// <returns>Added or not</returns>
        [HttpPost]
        [Route("Addbook")]
        public async Task<IActionResult> BookAdd([FromBody] BookModel bookModel)
        {
            try
            {
                int result = await this._bookManager.AddBooks(bookModel);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Response = "Book Added successfully." });
                }
                else
                {
                    return this.BadRequest(new { status = false, Response = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(e.Message);
            }
        }

        /// <summary>
        /// Delete Books
        /// </summary>
        /// <param name="bookId">bookId from query</param>
        /// <returns>deleted or not</returns>
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteBooks([FromQuery] int bookId)
        {
            try
            {
                int result = await this._bookManager.DeleteBook(bookId);
                if (result == 1)
                {
                    return this.Ok(new { status = true, Response = "Book deleted successfully." });
                }
                else
                {
                    return this.BadRequest(new { status = false, Response = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(e.Message);
            }
        }

        /// <summary>
        /// Update Books
        /// </summary>
        /// <param name="bookModel">passing bookModel</param>
        /// <returns>Update or not</returns>
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateBooks([FromBody] BookModel bookModel)
        {
            try
            {
                int result = await this._bookManager.UpdateBook(bookModel);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Response = "Book Updated successfully." });
                }
                else
                {
                    return this.BadRequest(new { status = false, Response = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(e.Message);
            }
        }
    }
}
