
using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Interface;
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
    public class ModelCreateUserService : IModelCreateUserService
    {
        private readonly IModelCreateUserRepository _modelCreateUserRepository;

        public ModelCreateUserService(IModelCreateUserRepository modelUserRepository)
        {
            _modelCreateUserRepository = modelUserRepository;
        }
        public async Task<ModelUsers> CreateUserAsync(CreateUserDTO userDto)
        {
            // Example password salting logic (for demo only)
            var salt = Guid.NewGuid().ToString("N").Substring(0, 8);
            var hashedPassword = HashPassword(userDto.Password, salt);

            var newUser = new ModelUsers
            {
                Name = userDto.Name,
                EmailId = userDto.EmailId,
                Password = hashedPassword,
                PasswordSalt = salt,
                IsActive = true,
                InsertBy = 1,
                InsertDate = DateTime.Now,
                UpdateBy = 1,
                UpdateDate = DateTime.Now
            };

            return await _modelCreateUserRepository.AddUserAsync(newUser);
        }



        private string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            return Convert.ToBase64String(sha256.ComputeHash(bytes));
        }

    }

}


