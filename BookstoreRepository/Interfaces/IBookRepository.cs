using BookstoreModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreRepository.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookModel>> AllBooks();
        Task<int> AddBook(BookModel bookModel);
    }
}