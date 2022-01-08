using BookstoreModel;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookModel>> AllBooks();
        Task<int> AddBook(BookModel bookModel);
        Task<int> Delete(int bookId);
        Task<int> Update(BookModel bookModel);
        Task<BookModel> GetOne(int bookId);
        Task<string> UploadImg(int bookId, IFormFile file);
    }
}