using BookstoreManager.Interfaces;
using BookstoreModel;
using BookstoreRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteManager _favoriteManager;
        private readonly IAuth _auth;
        private StringValues head;
        public FavoriteController(IFavoriteManager favoriteManager, IAuth auth)
        {
            this._favoriteManager = favoriteManager;
            this._auth = auth;
        }

        /// <summary>
        /// Adding to favorite
        /// </summary>
        /// <param name="favorite">passing favorite</param>
        /// <returns>added or not</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> FavoriteBookAdd([FromQuery] int bookId)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int userId = await this._auth.ValidateJwtToken(this.head);
                int result = await this._favoriteManager.AddToFavorite(userId, bookId);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Message = "Added to favorite." });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// removing from favorite
        /// </summary>
        /// <param name="favorite">passing favorite</param>
        /// <returns>added or not</returns>
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteFromFavorite([FromQuery] int bookId)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int userId = await this._auth.ValidateJwtToken(this.head);
                int result = await this._favoriteManager.RemoveFromFavorite(userId, bookId);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Message = "Removed from favorite." });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// All favorite list
        /// </summary>
        /// <param name="userId">passing UserId</param>
        /// <returns> All favorite list</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int id = await this._auth.ValidateJwtToken(this.head);
                IEnumerable<BookModel> result = await this._favoriteManager.FavoriteList(id);
                if (result.Count() >= 1)
                {
                    return this.Ok(new { status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }
    }
}
