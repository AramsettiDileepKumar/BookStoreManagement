using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Exceptions;
using ModelLayer.Models.User;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace RepositoryLayer.Services
{
    public class UserServiceRL:IUserRL
    {
        private readonly DapperContext _context;
        private readonly IConfiguration _configuration;
        public UserServiceRL(DapperContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<bool> AddUser(UserRequest request)
        {
            try
            {
                UserEntity Entity = new UserEntity
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = Encrypt(request.Password),
                    MobileNumber = request.MobileNumber,
                };
                var parameters = new DynamicParameters();
                parameters.Add("@Name", Entity.Name);
                parameters.Add("@Email", Entity.Email);
                parameters.Add("@Password", Entity.Password);
                parameters.Add("@MobileNumber",Entity.MobileNumber);
                var result = await _context.CreateConnection().ExecuteAsync("AddUser_sp",parameters);
                return result > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> Login(UserLoginRequest userLogin)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Email", userLogin.Email);
                var user = await _context.CreateConnection().QueryFirstOrDefaultAsync<UserEntity>("login_sp",parameters);
                if (user == null)
                {
                    throw new Exception($"User with email '{userLogin.Email}' not found.");
                }
                if (Encrypt(userLogin.Password).Equals(user.Password))
                {
                    return GenerateJwtToken(user);
                }
                else
                {
                    throw new Exception("Incorrect password");
                }   
            }
            catch(SqlException ex)
            {
                throw new Exception (ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Encrypt(string password)
        {
            byte[] refer = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(refer);
        }

        public string GenerateJwtToken(UserEntity user)
         {
            if (user == null)
            {
                throw new UserNotFoundException(nameof(user), "User cannot be null.");
            }
            if (_configuration == null)
            {
                throw new InvalidOperationException("Configuration is null. Make sure it's properly initialized.");
            }
            string jwtSecret = _configuration["JwtSettings:Secret"];
            if (string.IsNullOrEmpty(jwtSecret))
            {
                throw new InvalidOperationException("JWT secret key is null or empty. Make sure it's properly configured.");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            if (key.Length < 32)
            {
                throw new ArgumentException("JWT secret key must be at least 256 bits (32 bytes)");
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
