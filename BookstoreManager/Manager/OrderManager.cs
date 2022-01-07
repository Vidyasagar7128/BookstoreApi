using BookstoreManager.Interfaces;
using BookstoreModel;
using BookstoreRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreManager.Manager
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository _orderRepository;
        public OrderManager(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        /// <summary>
        /// order books
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <param name="bookId">passing bookId</param>
        /// <param name="addressId">passing addressId</param>
        /// <returns>ordered or not</returns>
        public async Task<int> AddOrder(OrderModel order, int userId)
        {
            try
            {
                return await this._orderRepository.Add(order, userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Cancle order
        /// </summary>
        /// <param name="userId">passing userId</param>
        /// <param name="orderId">passing orderId</param>
        /// <returns>order cancle or not</returns>
        public async Task<int> CancleOrder(int userId, int orderId)
        {
            try
            {
                return await this._orderRepository.Cancle(userId, orderId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
