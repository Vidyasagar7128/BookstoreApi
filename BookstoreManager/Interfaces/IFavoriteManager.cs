using BookstoreModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface IFavoriteManager
    {
        Task<int> AddToFavorite(int userId, int bookId);
        Task<IEnumerable<BookModel>> FavoriteList(int userId);
        Task<int> RemoveFromFavorite(int userId, int bookId);
    }
}