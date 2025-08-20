using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MicroService_Template.Application.Services
{
    public class ForgotPasswordService : IForgotPasswordService
    {
      private readonly IForgotPasswordDbRepository _forgotPasswordRepository;
        public ForgotPasswordService(IForgotPasswordDbRepository forgotPasswordRepository)
        {
            _forgotPasswordRepository = forgotPasswordRepository
                ?? throw new ArgumentNullException(nameof(forgotPasswordRepository));
        }
        public async Task<ModelUsers> CheckEmailidAsync(string email)
        {
            var emailid = await _forgotPasswordRepository.GetByEmailAsync(email);
            if (emailid == null)
            {
                throw new KeyNotFoundException($"User E-Mail ID {email} not found.");
            }

            return emailid;
        }
        public async Task<ModelUsers> UpdatePasswordAsync(string email, ModelUsers modelUsers)
        {
            var user = await _forgotPasswordRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new KeyNotFoundException($"User E-Mail ID {email} not found.");
            }
            var salt = Guid.NewGuid().ToString("N").Substring(0, 8);
            user.Password = HashPassword(modelUsers.Password, salt);
            user.PasswordSalt=salt;
            await _forgotPasswordRepository.UpdateAsync(user);

            return user;
        }
        private string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            return Convert.ToBase64String(sha256.ComputeHash(bytes));
        }

    }
}

