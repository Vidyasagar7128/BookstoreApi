using BookstoreManager.Interfaces;
using BookstoreModel;
using BookstoreRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreManager.Manager
{
    public class ReviewsManager : IReviewsManager
    {
        private readonly IReviewsRepository _reviewsRepository;
        public ReviewsManager(IReviewsRepository reviewsRepository)
        {
            this._reviewsRepository = reviewsRepository;
        }

        /// <summary>
        /// Adding reviews
        /// </summary>
        /// <param name="reviews">passing reviews model</param>
        /// <returns>added or not</returns>
        public async Task<int> AddReview(ReviewsModel reviews)
        {
            try
            {
                return await this._reviewsRepository.Add(reviews);
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
        public async Task<IEnumerable<ReviewsModel>> GetAllReviews(int bookId)
        {
            try
            {
                return await this._reviewsRepository.allReviews(bookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
