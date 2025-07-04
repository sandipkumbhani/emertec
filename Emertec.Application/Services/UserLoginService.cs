using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Extension.Interface;
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

namespace MicroService_Template.Application.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly string _JwtKey;
        private readonly string _JwtIssuer;
        private readonly string _JwtAudience;
        private readonly int _JwtExpiry;
        public UserLoginService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _JwtKey = _configuration["Jwt:Key"];
            _JwtIssuer = _configuration["Jwt:Issuer"];
            _JwtAudience = _configuration["Jwt:Audience"];
            _JwtExpiry = int.Parse(_configuration["Jwt:ExpiryMinutes"] ?? "60");
        }

        public async Task<string> LoginAsync(LoginUser dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || string.IsNullOrEmpty(user.Salt)) return null;

            var hashedInputPassword = HashPassword(dto.Password, user.Salt);
            if (user.PasswordHash != hashedInputPassword) return null;

            return GenerateJWTToken(user);
        }



        private string GenerateJWTToken(ModelUserLogin users)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, users.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, users.Email ?? string.Empty),
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


        private string HashPassword(string password, string saltBase64)
        {
            byte[] salt = Convert.FromBase64String(saltBase64);

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }


    }
}
