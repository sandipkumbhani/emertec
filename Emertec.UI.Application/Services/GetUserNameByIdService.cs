using Emertec.UI.Application.Interface;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Application.Services
{
    public class GetUserNameByIdService : IGetUserNameByIdService
    {
        private readonly IGetUserNameByIdRepository _getUserNameByIdRepository;
        public GetUserNameByIdService(IGetUserNameByIdRepository getUserNameByIdRepository)
        {
            _getUserNameByIdRepository = getUserNameByIdRepository ?? throw new ArgumentNullException(nameof(getUserNameByIdRepository));
        }
        public async Task<ModelUsers> GetUserNameByIdAsync(long userId)
        {
            if(userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");
            }
         
            var userName = await _getUserNameByIdRepository.GetUserNameAsync(userId);
            if (userName == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            return userName;
        }
    }
}
