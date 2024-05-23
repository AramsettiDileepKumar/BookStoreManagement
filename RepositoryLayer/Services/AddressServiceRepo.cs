using Dapper;
using ModelLayer.Models.AddressModel;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class AddressServiceRepo:IAddressRepo
    {
        private readonly DapperContext _context;

        public AddressServiceRepo(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AddressWithUserDetails>> GetAddresses(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UserId",userId);
            return await _context.CreateConnection().QueryAsync<AddressWithUserDetails>("GetAddress_SP", new { UserId = userId });    
        }

        public async Task<AddressEntity> GetAddressById(int addressId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("AddressId", addressId);
            return await _context.CreateConnection().QueryFirstOrDefaultAsync<AddressEntity>("GetAddressById_SP", parameters);
        }

        public async Task AddAddress(AddressEntity address)
        {

            var parameters = new DynamicParameters();
            parameters.Add("Address", address.Address);
            parameters.Add("City", address.City);
            parameters.Add("State",address.State);
            parameters.Add("Type",address.Type);
            parameters.Add("UserId",address.UserId);
            await _context.CreateConnection().ExecuteAsync("addAddress_SP", parameters);        
        }

        public async Task UpdateAddress(AddressEntity address)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Address", address.Address);
            parameters.Add("City", address.City);
            parameters.Add("State", address.State);
            parameters.Add("Type", address.Type);
            parameters.Add("UserId", address.UserId);
            parameters.Add("AddressId",address.AddressId);
            await _context.CreateConnection().ExecuteAsync("UpdateAddress_SP",parameters);
        }


        public async Task DeleteAddress(int addressId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("AddressId", addressId);
            await _context.CreateConnection().ExecuteAsync("deleteAddress_SP",parameters);
        }
    }
}
