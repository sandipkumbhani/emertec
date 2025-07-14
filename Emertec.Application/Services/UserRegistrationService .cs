using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;

        public UserRegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterAsync(RegisterUserDTO registerUserDTO)
        {
<<<<<<< Updated upstream
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null) return false;

            var (hash, salt) = CreateHashedPassword(dto.Password);
=======
            var existingUser = await _userRepository.GetByEmailAsync(registerUserDTO.Email);
            if (existingUser != null) return false;

            var (hash, salt) = CreateHashedPassword(registerUserDTO.Password);
>>>>>>> Stashed changes

            var user = new ModelUserLogin
            {
                Id = Guid.NewGuid(),
<<<<<<< Updated upstream
                Username = dto.Username,
                Email = dto.Email,
=======
                Username = registerUserDTO.Username,
                Email = registerUserDTO.Email,
>>>>>>> Stashed changes
                PasswordHash = hash,
                Salt = salt
            };

            await _userRepository.AddAsync(user);
            return true;
        }
        private (string hash, string salt) CreateHashedPassword(string password)
        {
            byte[] saltBytes = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            string salt = Convert.ToBase64String(saltBytes);

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return (hash, salt);

        }

    }

}
