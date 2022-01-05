using BookstoreManager.Interfaces;
using BookstoreRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartManager _cartManager;
        private readonly IAuth _auth;
        private StringValues head;
        public CartController(ICartManager cartManager, IAuth auth)
        {
            this._cartManager = cartManager;
            this._auth = auth;
        }

        /// <summary>
        /// books adding to cart
        /// </summary>
        /// <param name="bookId">for query</param>
        /// <returns>added or not</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> BookAddToCart([FromQuery] int bookId)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int userId = await this._auth.ValidateJwtToken(this.head);
                int result = await this._cartManager.Bookadd(userId, bookId);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Message = "Book added to cart." });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(e.Message);
            }
        }

        [HttpDelete]
        [Route("remove")]
        public async Task<IActionResult> BookRemoveFromCart([FromQuery] int bookId)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int userId = await this._auth.ValidateJwtToken(this.head);
                int result = await this._cartManager.DeletefromCart(userId, bookId);
                if (result == 1)
                {
                    return this.Ok(new { status = true, Message = "Book deleted from cart." });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = "Something went wrong." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(e.Message);
            }
        }

        /// <summary>
        /// Increase Quantity
        /// </summary>
        /// <param name="bookId">passing bookId</param>
        /// <returns>Increamented or not</returns>
        [HttpPut]
        [Route("increase")]
        public async Task<IActionResult> IncreaseQuantity([FromQuery] int bookId)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int id = await this._auth.ValidateJwtToken(this.head);
                int result = await this._cartManager.IncreamentQuantity(id, bookId);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Message = "Quantity Increamented" });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
