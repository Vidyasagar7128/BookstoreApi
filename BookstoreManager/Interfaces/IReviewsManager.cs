using BookstoreModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface IReviewsManager
    {
        Task<int> AddReview(ReviewsModel reviews);
    }
}