using ModelLayer.Models.CartModel;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        Task<IEnumerable<BookEntity>> GetCartBooks(int userId);
        Task<IEnumerable<BookEntity>> AddToCart(CartRequest cartRequest, int userId);
        Task<CartRequest> UpdateQuantity(int userId, CartRequest cartRequest);
        Task<bool> DeleteCart(int userId, int Bookid);
    }
}
