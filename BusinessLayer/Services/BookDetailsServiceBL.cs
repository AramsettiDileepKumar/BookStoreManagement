using BusinessLayer.Interfaces;
using ModelLayer.Models.BookModel;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BookDetailsServiceBL:IBookDetailsBL
    {
        private readonly IBookDetailsRL bookDetails;
        public BookDetailsServiceBL(IBookDetailsRL book)
        {
            bookDetails = book;
        }
        public async Task<bool> addBook(BookDetailsRequest request)
        {
            return await bookDetails.addBook(request);  
        }
        public async Task<IEnumerable<BookEntity>> getAllBook()
        {
            return await bookDetails.getAllBook();
        }
        public async Task<BookEntity> getBookById(int bookId)
        {
            return await bookDetails.getBookById(bookId);
        }
    }
}
