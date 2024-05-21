using Dapper;
using ModelLayer.Models.CartModel;
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
    public class CartServiceRL:ICartRL
    {
        private readonly DapperContext _context;
        public CartServiceRL(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BookEntity>> GetCartBooks(int userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                var cartBooks = await _context.CreateConnection().QueryAsync<BookEntity>("getCartBooks_SP", parameters);
                return cartBooks.ToList();
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
        public async Task<IEnumerable<BookEntity>> AddToCart(CartRequest cartRequest, int userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Quantity", cartRequest.Quantity);
                parameters.Add("@UserId", userId);
                parameters.Add("@BookId", cartRequest.BookId);
                await _context.CreateConnection().ExecuteAsync("AddToCart_SP", parameters);
                return await GetCartBooks(userId);
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
        public async Task<CartRequest> UpdateQuantity(int userId, CartRequest cartRequest)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Quantity", cartRequest.Quantity);
                parameters.Add("@UserId", userId);
                parameters.Add("@BookId", cartRequest.BookId);
                await _context.CreateConnection().ExecuteAsync("UpdateCart_SP", parameters);
                return cartRequest;
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
        public async Task<bool> DeleteCart(int userId, int BookId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BookId", BookId);
                parameters.Add("@UserId", userId);
                return await _context.CreateConnection().ExecuteAsync("deleteCart_SP", parameters) > 0;
        
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
        private async Task<bool> IsCartItemExists(int userId, int CartId)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM CartEntity WHERE UserId = @UserId AND CartId = @cartId";
                using (var connection = _context.CreateConnection())
                {
                    var count = await connection.ExecuteScalarAsync<int>(query, new { UserId = userId, cartId = CartId });
                    return count > 0;
                }
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
