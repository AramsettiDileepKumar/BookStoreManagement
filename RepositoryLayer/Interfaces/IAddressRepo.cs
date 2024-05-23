using ModelLayer.Models.AddressModel;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRepo
    {
        Task<IEnumerable<AddressWithUserDetails>> GetAddresses(int userId);
        Task<AddressEntity> GetAddressById(int addressId);
        Task AddAddress(AddressEntity address);
        Task UpdateAddress(AddressEntity address);
        Task DeleteAddress(int addressId);
    }
}
