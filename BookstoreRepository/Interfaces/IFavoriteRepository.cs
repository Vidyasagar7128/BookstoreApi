using BookstoreModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<int> Add(int userId, int bookId);
        Task<int> Remove(int userId, int bookId);
        Task<IEnumerable<BookModel>> all(int userId);
    }
}