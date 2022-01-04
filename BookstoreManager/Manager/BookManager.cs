using BookstoreManager.Interfaces;
using BookstoreModel;
using BookstoreRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreManager.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        /// <summary>
        /// Getting all books
        /// </summary>
        /// <returns>List of books</returns>
        public async Task<IEnumerable<BookModel>> Books()
        {
            try
            {
                return await this._bookRepository.AllBooks();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Adding Books
        /// </summary>
        /// <param name="bookModel">book object</param>
        /// <returns>int</returns>
        public async Task<int> AddBooks(BookModel bookModel)
        {
            try
            {
                return await this._bookRepository.AddBook(bookModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Delete book
        /// </summary>
        /// <param name="bookId">int id</param>
        /// <returns>deleted or not</returns>
        public async Task<int> DeleteBook(int bookId)
        {
            try
            {
                return await this._bookRepository.Delete(bookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
