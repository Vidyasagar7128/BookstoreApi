using BookstoreModel;
using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface IReviewsManager
    {
        Task<int> AddReview(ReviewsModel reviews);
    }
}