using Dapper;
using Microsoft.Extensions.Configuration;
using ModelLayer.Models.BookModel;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class BookDetailsServiceRL:IBookDetailsRL
    {
        private readonly DapperContext _context;
        public BookDetailsServiceRL(DapperContext context)
        {
            _context = context;
        }
        public async Task<bool> addBook(BookDetailsRequest book)
        {
            try
            {
                return await _context.CreateConnection().ExecuteAsync("InsertBookDetails_SP", book) > 0;
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<BookEntity>> getAllBook()
        {
            try
            {
                return await _context.CreateConnection().QueryAsync<BookEntity>("getAllBooks_SP");
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<BookEntity> getBookById(int bookId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BookId", bookId);
                return await _context.CreateConnection().QueryFirstOrDefaultAsync<BookEntity>("getBookByBookId_SP", parameters);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
