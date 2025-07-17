
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
    public class MenuMasterServices : IMenuMasterServices
    {
        private readonly IMenuMasterRepository _menuMasterRepository;

        public MenuMasterServices(IMenuMasterRepository menuMasterRepository)
        {
            _menuMasterRepository = menuMasterRepository;
        }
        public async Task<List<ModelMenuMaster>> GetAllMenuMasterAsync()
        {
            return await _menuMasterRepository.GetAllMenuAsync();
        }
        public async Task<string> AddMenuAsync(ModelMenuMaster modelMenuMaster)
        {
            return await _menuMasterRepository.AddMenuAsync(modelMenuMaster);
        }
    }
}
