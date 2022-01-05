using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface ICartManager
    {
        Task<int> Bookadd(int userId, int bookId);
        Task<int> DeletefromCart(int userId, int bookId);
        Task<int> IncreamentQuantity(int userId, int bookId);
        Task<int> DecreamentQuantity(int userId, int bookId);
    }
}