using BookstoreManager.Interfaces;
using BookstoreRepository.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookstoreManager.Manager
{
    public class CartManager : ICartManager
    {
        private readonly ICartRepository _cartRepository;
        public CartManager(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }

        /// <summary>
        /// Adding book to cart
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="bookId">bookId</param>
        /// <returns>added or not</returns>
        public async Task<int> Bookadd(int userId, int bookId)
        {
            try
            {
                return await this._cartRepository.Add(userId, bookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
