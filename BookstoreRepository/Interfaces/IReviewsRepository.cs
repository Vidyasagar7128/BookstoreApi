using BookstoreModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface IReviewsRepository
    {
        Task<int> Add(ReviewsModel reviews);
        Task<IEnumerable<ReviewsModel>> allReviews(int bookId);
    }
}