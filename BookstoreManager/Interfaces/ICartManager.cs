using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface ICartManager
    {
        Task<int> Bookadd(int userId, int bookId);
    }
}