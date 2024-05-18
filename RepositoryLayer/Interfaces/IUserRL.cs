using ModelLayer.Models.User;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public Task<bool> AddUser(UserRequest request);
        public Task<string> Login(UserLoginRequest userLogin);
    }
}
