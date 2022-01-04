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
    }
}
