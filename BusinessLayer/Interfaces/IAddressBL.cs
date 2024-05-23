using ModelLayer.Models.AddressModel;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAddressBL
    {
        Task<IEnumerable<AddressWithUserDetails>> GetAddresses(int userId);
        Task<AddressEntity> GetAddressById(int addressId);
        Task AddAddress(AddressRequest addressRequest, int userId);
        Task UpdateAddress(int addressId, AddressRequest addressRequest, int userId);
        Task DeleteAddress(int addressId);
    }
}
