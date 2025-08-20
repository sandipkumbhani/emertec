using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Services
{
    public class GetLoginUserNameService : IGetLoginUserNameService
    {
        private readonly IGetLoginUserNameRepository _getLoginUserNameRepository;
        public GetLoginUserNameService(IGetLoginUserNameRepository getLoginUserNameRepository)
        {
            _getLoginUserNameRepository = getLoginUserNameRepository;
        }
        public async Task<ModelUsers> GetLoginUserNameAsync(long userId)
        {
            return await _getLoginUserNameRepository.GetUserNameAsync(userId);
        }
    }
}
