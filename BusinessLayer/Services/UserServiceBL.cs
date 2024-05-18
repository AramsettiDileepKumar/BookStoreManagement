using BusinessLayer.Interfaces;
using Microsoft.Win32;
using ModelLayer.Models.User;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserServiceBL:IUserBL
    {
        private readonly IUserRL _User;
        public UserServiceBL(IUserRL user)
        {
            _User = user;
        }
        public Task<bool> AddUser(UserRequest request)
        {
            return _User.AddUser(request);
        }
        public async Task<string> Login(UserLoginRequest userLogin)
        {
            return await _User.Login(userLogin);
        }
    }
}
