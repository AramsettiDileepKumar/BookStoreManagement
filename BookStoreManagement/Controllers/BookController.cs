using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models.BookModel;
using ModelLayer.Models.Response;
using RepositoryLayer.Entities;

namespace BookStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookDetailsBL bookDetails;
        public BookController(IBookDetailsBL bookDetails)
        {
            this.bookDetails = bookDetails;
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookDetailsRequest book)
        {
            try
            {
                await bookDetails.addBook(book);

                var response = new ResponseModel<string>
                {
                    Success = true,
                    Message = "Book Added Successfully"

                };
                return Ok(response);
            }
            catch (Exception ex)
            {

                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetBook()
        {
            try
            {
                var book = await bookDetails.getAllBook();

                var response = new ResponseModel<IEnumerable<BookEntity>>
                {
                    Success = true,
                    Message = "All Book Details Fetched Succesfully",
                    Data = book

                };
                return Ok(book);
            }
            catch (Exception ex)
            {

                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }

        }
        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookbyid(int bookId)
        {
            try
            {
                var result = await bookDetails.getBookById(bookId);

                var response = new ResponseModel<BookEntity>
                {
                    Success = true,
                    Message = "Fetched Book Details By Book Id",
                    Data = result
                };
                return Ok(result);
            }
            catch (Exception ex)
            {

                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }

        }
    }
}
