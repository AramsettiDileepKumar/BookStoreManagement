using ModelLayer.Models.BookModel;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IBookDetailsRL
    {
        Task<bool> addBook(BookDetailsRequest request);
        Task<IEnumerable<BookEntity>> getAllBook();
        Task<BookEntity> getBookById(int bookId);
    }
}
