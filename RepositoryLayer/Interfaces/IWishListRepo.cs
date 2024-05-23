using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRepo
    {
        Task<bool> AddToWishList(int userId, int BookId);
        Task<IEnumerable<BookEntity>> GetWishlistBooks(int userId);
        Task<bool> DeleteWishlist(int userId,int BookId);
    }
}
