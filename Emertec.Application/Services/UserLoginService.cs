using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Extension.Interface;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MicroService_Template.Domain.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IModelUserLoginRepository _userLoginRepository;
        private readonly IConfiguration _configuration;
        private readonly string _JwtKey;
        private readonly string _JwtIssuer;
        private readonly string _JwtAudience;
        private readonly int _JwtExpiry;
        public UserLoginService(IModelUserLoginRepository userRepository, IConfiguration configuration)
        {
            _userLoginRepository = userRepository;
            _configuration = configuration;
            _JwtExpiry = int.Parse(_configuration["Jwt:ExpiryMinutes"] ?? "60");
        }

        public async Task<LoginUserDTO?> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userLoginRepository.GetByEmailAsync(email);

                if (user == null || string.IsNullOrEmpty(user.PasswordSalt))
                {
                    return null;
                }
                var hashedPassword = HashPassword(password, user.PasswordSalt);

                if (user.Password != hashedPassword)
                {
                    return null;
                }
                var role = await _userLoginRepository.GetUserWithRoleAsync(user.UserRoleId);

                var token = GenerateJWTToken(user,role.Name);


                return new LoginUserDTO
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    EmailId = user.EmailId,
                    Password = hashedPassword,
                    Token = token,
                    UserRoleId = user.UserRoleId,
                    UserRoleName = role.Name,
                    IsActive = user.IsActive,
                    UpdateBy = user.UpdateBy,
                    UpdateDate = user.UpdateDate,
                    InsertBy = user.InsertBy,
                    InsertDate = user.InsertDate
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
            }
            return null;
        }

        private string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            return Convert.ToBase64String(sha256.ComputeHash(bytes));
        }
        private string GenerateJWTToken(ModelUsers modelUsers, string roleName)
        {
            var claims = new[]
            {
             
            new Claim(JwtRegisteredClaimNames.Sub, modelUsers.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, modelUsers.EmailId ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, roleName),

    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));        
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }

}


