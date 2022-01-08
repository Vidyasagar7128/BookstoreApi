using BookstoreModel;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface IReviewsRepository
    {
        Task<int> Add(ReviewsModel reviews);
    }
}