
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MicroService_Template.Application.Services
{
    public class ModelCreateUserService : IModelCreateUserService
    {
        private readonly IModelCreateUserRepository _modelCreateUserRepository;

        public ModelCreateUserService(IModelCreateUserRepository modelUserRepository)
        {
            _modelCreateUserRepository = modelUserRepository;
        }
        public async Task<ModelUsers> CreateUserAsync(ModelUsers modelUsers)
        {
            bool emailExists = await _modelCreateUserRepository.EmailExistsAsync(modelUsers.EmailId);
            if (emailExists)
            {
                throw new InvalidOperationException("Email already exists.");
            }

            var salt = Guid.NewGuid().ToString("N").Substring(0, 8);
            var hashedPassword = HashPassword(modelUsers.Password, salt);

            var newUser = new ModelUsers
            {
                Name = modelUsers.Name,
                EmailId = modelUsers.EmailId,
                Password = hashedPassword,
                PasswordSalt = salt,
                UserRoleId = modelUsers.UserRoleId,
                IsActive = true,
                InsertBy = 1,
                InsertDate = DateTime.Now,
                UpdateBy = 1,
                UpdateDate = DateTime.Now
            };

            return await _modelCreateUserRepository.AddUserAsync(newUser);
        }
        public async Task<List<ModelUsers>> GetAllUsersAsync()
        {
            var users = await _modelCreateUserRepository.GetAllUsersAsync();

            return users.Select(user => new ModelUsers
            {
                UserId = user.UserId,
                Name = user.Name,
                EmailId = user.EmailId,
                UserRoleId = user.UserRoleId,
                IsActive = user.IsActive,
                InsertBy = user.InsertBy,
                InsertDate = user.InsertDate,
                UpdateBy = user.UpdateBy,
                UpdateDate = user.UpdateDate,
                UserRole = user.UserRole

            }).ToList();
        }
        public async Task DeleteUserById(int id)
        {
            var deleteUser = _modelCreateUserRepository.GetUserById(id);
            if (deleteUser == null)
            {
                throw new KeyNotFoundException($"User ID {id} not found.");
            }

           await _modelCreateUserRepository.DeleteAsync(deleteUser);
        }
        public async Task<ModelUsers> UpdateUserAsync(int userid, ModelUsers modelUsers)
        {

            var userExisting = _modelCreateUserRepository.GetUserById(userid);

            if (userExisting == null)
            {
                throw new Exception($"User with ID {userid} not found.");
            }
            userExisting.Name = modelUsers.Name;
            userExisting.EmailId = modelUsers.EmailId;
            userExisting.IsActive =true;
            userExisting.InsertBy = 1;
            userExisting.InsertDate = DateTime.UtcNow; 
            userExisting.UpdateBy = 1;
            userExisting.UpdateDate = DateTime.UtcNow; 


           await _modelCreateUserRepository.UserUpdateAsync(userExisting);

            return userExisting;
        }
        public ModelUsers GetUserDetailsById(int userid)
        {
            var UserDetails = _modelCreateUserRepository.GetUserById(userid);
            if (UserDetails == null)
            {
                throw new KeyNotFoundException($"User Id with ID {userid} not found.");
            }

            return new ModelUsers
            {
                UserId = UserDetails.UserId,
                Name = UserDetails.Name,
                EmailId = UserDetails.EmailId,
                UserRoleId = UserDetails.UserRoleId,
                IsActive = UserDetails.IsActive,
                InsertBy = UserDetails.InsertBy,
                InsertDate = UserDetails.InsertDate,
                UpdateBy = UserDetails.UpdateBy,
                UpdateDate = UserDetails.UpdateDate,
                UserRole = UserDetails.UserRole

            };
        }



        private string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            return Convert.ToBase64String(sha256.ComputeHash(bytes));
        }

    }

}


