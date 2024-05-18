using BusinessLayer.Interfaces;
using ModelLayer.Models.CartModel;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CartServiceBL:ICartBL
    {
        private readonly ICartRL _Cart;

        public CartServiceBL(ICartRL shoppingCartRepository)
        {
            _Cart = shoppingCartRepository;
        }

        public async Task<IEnumerable<BookEntity>> GetCartBooks(int userId)
        {
            return await _Cart.GetCartBooks(userId);
        }

        public async Task<IEnumerable<BookEntity>> AddToCart(CartRequest cartRequest, int userId)
        {
            return await _Cart.AddToCart(cartRequest, userId);
        }

        public async Task<CartRequest> UpdateQuantity(int userId, CartRequest cartRequest)
        {
            return await _Cart.UpdateQuantity(userId, cartRequest);
        }

        public async Task<bool> DeleteCart(int userId, int id)
        {
            return await _Cart.DeleteCart(userId, id);
        }
    }
}
