using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class WishListServiceBL:IWishListBL
    {
        private IWishListRepo listRepo;
        public WishListServiceBL(IWishListRepo listRepo)
        {
            this.listRepo = listRepo;
        }
        public async Task<bool> AddToWishList(int UserId,int BookId)
        {
            return await listRepo.AddToWishList(UserId, BookId);
        }
        public async Task<IEnumerable<BookEntity>> GetWishlistBooks(int userId)
        {
            return await listRepo.GetWishlistBooks(userId);
        }
        public async Task<bool> DeleteWishlist(int UserId,int wishListId)
        {
            return await listRepo.DeleteWishlist(UserId,wishListId);
        }
    }
}
