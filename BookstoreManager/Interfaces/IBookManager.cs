﻿using BookstoreModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreManager.Interfaces
{
    public interface IBookManager
    {
        Task<IEnumerable<BookModel>> Books();
        Task<int> AddBooks(BookModel bookModel);
    }
}