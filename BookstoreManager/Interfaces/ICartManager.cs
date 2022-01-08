using BookstoreModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface ICartManager
    {
        Task<int> Bookadd(int userId, int bookId, int price);
        Task<int> DeletefromCart(int userId, int bookId);
        Task<int> IncreamentQuantity(QuantityModel quantity);
        Task<IEnumerable<CartModel>> GetCartItems(int userId);
    }
}