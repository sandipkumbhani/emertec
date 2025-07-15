using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Extension.Interface;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IModelUserLoginRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly string _JwtKey;
        private readonly string _JwtIssuer;
        private readonly string _JwtAudience;
        private readonly int _JwtExpiry;
        public UserLoginService(IModelUserLoginRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _JwtKey = _configuration["Jwt:Key"];
            _JwtIssuer = _configuration["Jwt:Issuer"];
            _JwtAudience = _configuration["Jwt:Audience"];
            _JwtExpiry = int.Parse(_configuration["Jwt:ExpiryMinutes"] ?? "60");
        }

        public async Task<LoginUserDTO?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || string.IsNullOrEmpty(user.PasswordSalt))
                return null;

            var hashedPassword = HashPassword(password, user.PasswordSalt);

            if (user.Password != hashedPassword)
                return null;

            var token = GenerateJWTToken(user); 

            return new LoginUserDTO
            {

                UserId = user.UserId,
                Name = user.Name,
                Password=hashedPassword,
                EmailId = user.EmailId,
                Token = token 
            };
        }
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Select(user => new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                EmailId = user.EmailId,
                UserRoleId = user.UserRoleId,
                IsActive = user.IsActive
            }).ToList();
        }


        private string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            return Convert.ToBase64String(sha256.ComputeHash(bytes));
        }




        private string GenerateJWTToken(ModelUsers modelUsers)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, modelUsers.UserId.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, modelUsers.EmailId ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _JwtIssuer,
                audience: _JwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_JwtExpiry),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }

}
    

