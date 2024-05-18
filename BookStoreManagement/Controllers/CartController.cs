using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models.CartModel;
using ModelLayer.Models.Response;
using RepositoryLayer.Entities;
using System.Security.Claims;

namespace BookStoreManagement.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CartController : ControllerBase
    {
        private readonly ICartBL _Cart;

        public CartController(ICartBL cartService)
        {
            _Cart = cartService;
        }

        [HttpGet("GetCartBooks")]
        public async Task<IActionResult> GetCartBooks()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var cartBooks = await _Cart.GetCartBooks(userId);
            var response = new ResponseModel<IEnumerable<BookEntity>> { Message = "Retrieved books successfully", Data = cartBooks };
            return Ok(response);
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] CartRequest cartRequest)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var updatedCart = await _Cart.AddToCart(cartRequest, userId);
            var response = new ResponseModel<IEnumerable<BookEntity>>{ Message = "Added to cart successfully", Data = updatedCart };
            return Ok(response);
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] CartRequest cartRequest)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var updatedCartRequest = await _Cart.UpdateQuantity(userId, cartRequest);
            var response = new ResponseModel<CartRequest> { Message = "Updated quantity successfully", Data = updatedCartRequest };
            return Ok(response);
        }

        [HttpDelete("DeleteCart")]
        public async Task<IActionResult> DeleteCart( int cartId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isDeleted = await _Cart.DeleteCart(userId, cartId);
            var response = new ResponseModel<bool> { Message = "Deleted from cart successfully", Data = isDeleted };
            return Ok(response);
        }
    }
}
