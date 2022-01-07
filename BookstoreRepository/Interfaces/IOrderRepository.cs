using BookstoreModel;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> Add(OrderModel order, int userId);
        Task<int> Cancle(int userId, int orderId);
    }
}