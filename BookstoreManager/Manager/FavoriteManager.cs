using BookstoreManager.Interfaces;
using BookstoreModel;
using BookstoreRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreManager.Manager
{
    public class FavoriteManager : IFavoriteManager
    {
        private readonly IFavoriteRepository _favoriteRepository;
        public FavoriteManager(IFavoriteRepository favoriteRepository)
        {
            this._favoriteRepository = favoriteRepository;
        }

        /// <summary>
        /// Adding to favorite
        /// </summary>
        /// <param name="favorite">passing favorite</param>
        /// <returns>added or not</returns>
        public async Task<int> AddToFavorite(int userId, int bookId)
        {
            try
            {
                return await this._favoriteRepository.Add(userId, bookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// removing from favorite
        /// </summary>
        /// <param name="favorite">passing favorite</param>
        /// <returns>added or not</returns>
        public async Task<int> RemoveFromFavorite(int userId, int bookId)
        {
            try
            {
                return await this._favoriteRepository.Remove(userId, bookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// All favorite list
        /// </summary>
        /// <param name="userId">passing UserId</param>
        /// <returns> All favorite list</returns>
        public async Task<IEnumerable<BookModel>> FavoriteList(int userId)
        {
            try
            {
                return await this._favoriteRepository.all(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
