using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface ICartRepository
    {
        Task<int> Add(int userId, int bookId);
        Task<int> Remove(int userId, int bookId);
    }
}