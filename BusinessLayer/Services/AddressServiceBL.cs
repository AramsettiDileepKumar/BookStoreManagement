using BusinessLayer.Interfaces;
using ModelLayer.Models.AddressModel;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AddressServiceBL:IAddressBL
    {
        private readonly IAddressRepo _addressRepository;

        public AddressServiceBL(IAddressRepo addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<IEnumerable<AddressWithUserDetails>> GetAddresses(int userId)
        {
            return await _addressRepository.GetAddresses(userId);
        }

        public async Task<AddressEntity> GetAddressById(int addressId)
        {
            return await _addressRepository.GetAddressById(addressId);
        }

        public async Task AddAddress(AddressRequest addressRequest, int userId)
        {
            var address = new AddressEntity
            {
                Address = addressRequest.address,
                City = addressRequest.city,
                State = addressRequest.state,
                Type = addressRequest.type,
                UserId = userId
            };
            await _addressRepository.AddAddress(address);
        }

        public async Task UpdateAddress(int addressId, AddressRequest addressRequest, int userId)
        {
            var address = await _addressRepository.GetAddressById(addressId);
            if (address == null) throw new Exception("Address not found");

            address.Address = addressRequest.address;
            address.City = addressRequest.city;
            address.State = addressRequest.state;
            address.Type = addressRequest.type;
            address.UserId = userId;

            await _addressRepository.UpdateAddress(address);
        }

        public async Task DeleteAddress(int addressId)
        {
            await _addressRepository.DeleteAddress(addressId);
        }
    }
}
