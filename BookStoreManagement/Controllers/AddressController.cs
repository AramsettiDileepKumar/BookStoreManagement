using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models.AddressModel;
using ModelLayer.Models.Response;
using RepositoryLayer.Entities;
using System.Data.SqlClient;
using System.Net;
using System.Security.Claims;

namespace BookStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL _addressService;

        public AddressController(IAddressBL addressService)
        {
            _addressService = addressService;

        }


        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var addresses = await _addressService.GetAddresses(userId);
                return Ok(new ResponseModel<IEnumerable<AddressWithUserDetails>>
                {
                    Message = "Addresses retrieved successfully.",
                    Data = addresses
                });
            }
            catch (Exception ex)
            {
               return Ok(ex.Message);
            }
        }

        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetAddressById(int addressId)
        {
            try
            {
                var address = await _addressService.GetAddressById(addressId);
                if (address == null)
                    return NotFound(new ResponseModel<string>
                    {
                        Message = "Address not found.",
                        Data = null
                    });

                return Ok(new ResponseModel<AddressEntity>
                {
                    Message = "Address retrieved successfully.",
                    Data = address
                });
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressRequest addressRequest)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                await _addressService.AddAddress(addressRequest, userId);
                var addedAddress = await _addressService.GetAddresses(userId);

                return Ok(new ResponseModel<IEnumerable<AddressWithUserDetails>>
                {
                    Message = "Address added successfully.",
                    Data = addedAddress
                });
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

       [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddress(int addressId, [FromBody] AddressRequest addressRequest)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                await _addressService.UpdateAddress(addressId, addressRequest, userId);
                var updatedAddress = await _addressService.GetAddressById(addressId);

                return Ok(new ResponseModel<AddressEntity>
                {
                    Message = "Address updated successfully.",
                    Data = updatedAddress
                });
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpDelete("{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            try
            {
                await _addressService.DeleteAddress(addressId);
                return Ok(new ResponseModel<string>
                {
                    Message = "Address deleted successfully.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
