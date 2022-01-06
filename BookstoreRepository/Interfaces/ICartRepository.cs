using BookstoreModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface ICartRepository
    {
        Task<int> Add(int userId, int bookId);
        Task<int> Remove(int userId, int bookId);
        Task<int> Increase(int userId, int bookId);
        Task<int> Decrease(int userId, int bookId);
        Task<IEnumerable<CartModel>> Items(int userId);
    }
}