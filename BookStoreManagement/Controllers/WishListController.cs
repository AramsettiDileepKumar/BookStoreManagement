using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models.Response;
using RepositoryLayer.Entities;
using System.Security.Claims;

namespace BookStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL _wishlistService;
        public WishListController(IWishListBL List)
        {
            _wishlistService = List;
        }
        [HttpPost("{BookId}")]
        public async Task<IActionResult> AddToWishlist(int BookId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var addedWishlist = await _wishlistService.AddToWishList(userId, BookId);
                var response = new ResponseModel<bool> { Message = "Added to wishlist successfully", Data = addedWishlist };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpGet("GetWishlistBooks")]
        public async Task<IActionResult> GetWishlistBooks()
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var wishlistBooks = await _wishlistService.GetWishlistBooks(userId);
                var response = new ResponseModel<IEnumerable<BookEntity>> { Message = "Retrieved wishlist books successfully", Data = wishlistBooks };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpDelete("DeleteWishlist/{wishlistId}")]
        public async Task<IActionResult> DeleteWishlist(int wishlistId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var isDeleted = await _wishlistService.DeleteWishlist(userId, wishlistId);
                var response = new ResponseModel<bool> { Message = "Deleted from wishlist successfully", Data = isDeleted };
                return Ok(response);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
