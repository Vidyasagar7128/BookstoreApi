using BookstoreModel;
using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface IOrderManager
    {
        Task<int> AddOrder(OrderModel order, int userId);
        Task<int> CancleOrder(int userId, int orderId);
    }
}