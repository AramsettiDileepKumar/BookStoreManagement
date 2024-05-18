using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models.Response;
using ModelLayer.Models.User;
using RepositoryLayer.Entities;
using System.Data.SqlClient;

namespace BookStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class UserController : ControllerBase
    {
        private readonly IUserBL UserBL;
        public UserController(IUserBL user)
        {
            UserBL=user;
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(UserRequest res)
        {
            try
            {
                var result = await UserBL.AddUser(res);
                if (result)
                {
                    var response = new ResponseModel<bool>
                    {
                        Success = true,
                        Message = "User Registration Successful",
                        Data=result
                    };
                    return Ok(response);
                }
                else
                {
                    return Ok("invalid input");
                }
            }
            catch (SqlException ex)
            {
                return Ok(ex.Message);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel<UserEntity> { Success = false, Message = ex.Message });
            }

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest user)
        {
            try
            {
                var token = await UserBL.Login(user);
                var response = new ResponseModel<string>
                {
                    Message = "Login Successful",
                    Data = token
                };
                return Ok(response);
            }
            catch (SqlException ex)
            {
                return Ok(ex.Message);
            }
            catch (Exception ex)
            {
               return Ok(ex.Message);
            }
        }
    }
   
}
