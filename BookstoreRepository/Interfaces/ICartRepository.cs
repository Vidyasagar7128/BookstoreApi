using BookstoreModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface ICartRepository
    {
        Task<int> Add(int userId, int bookId, int price);
        Task<int> Remove(int userId, int bookId);
        Task<int> Increase(QuantityModel quantity);
        Task<IEnumerable<CartModel>> Items(int userId);
    }
}