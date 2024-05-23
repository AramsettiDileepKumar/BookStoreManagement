using Dapper;
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
    public class WishListServiceRepo:IWishListRepo
    {
        private readonly DapperContext _context;
        public WishListServiceRepo(DapperContext dapperContext)
        {
            _context=dapperContext;
        }
     
        public async Task<bool> AddToWishList(int userId,int bookId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                parameters.Add("@BookId", bookId);
                return await _context.CreateConnection().ExecuteAsync("AddToWishList_SP", parameters) > 0;
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
           
            
        }
        public async Task<IEnumerable<BookEntity>> GetWishlistBooks(int userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserId", userId);
                return await _context.CreateConnection().QueryAsync<BookEntity>("getBooksFromWishList_SP", parameters);
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

        public async Task<bool> DeleteWishlist(int userId, int BookId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("UserId", userId);
                parameters.Add("BookId", BookId);
                var result = await _context.CreateConnection().ExecuteAsync("deleteWishList_SP", parameters);
                return result > 0;
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
