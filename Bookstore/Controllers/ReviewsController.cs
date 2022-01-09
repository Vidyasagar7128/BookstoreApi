using BookstoreManager.Interfaces;
using BookstoreModel;
using BookstoreRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : Controller
    {
        private readonly IReviewsManager _reviewsManager;
        private readonly IAuth _auth;
        private StringValues head;
        public ReviewsController(IReviewsManager reviewsManager, IAuth auth)
        {
            this._reviewsManager = reviewsManager;
            this._auth = auth;
        }

        /// <summary>
        /// Adding reviews
        /// </summary>
        /// <param name="reviews">passing reviews model</param>
        /// <returns>added or not</returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddReviewToBook([FromBody] ReviewsModel reviews)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out this.head);
                int userId = await this._auth.ValidateJwtToken(this.head);
                reviews.UserId = userId;
                int result = await this._reviewsManager.AddReview(reviews);
                if (result == -1)
                {
                    return this.Ok(new { status = true, Message = "Review added successfully." });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// All reviews
        /// </summary>
        /// <param name="bookId">passing bookId</param>
        /// <returns>List of reviews</returns>
        [HttpGet]
        [Route("reviews")]
        public async Task<IActionResult> AllReviews([FromQuery] int bookId)
        {
            try
            {
                IEnumerable<ReviewsModel> result = await this._reviewsManager.GetAllReviews(bookId);
                if (result.Count() > 0)
                {
                    return this.Ok(new { status = true, Data = result });
                }
                else
                {
                    return this.BadRequest(new { status = false, Message = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Message = e.Message });
            }
        }
    }
}
