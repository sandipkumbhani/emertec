
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
    public class MenuMappingService : IMenuMappingServices
    {
        private readonly IMenuMappingRepository _menuMappingRepository;

        public MenuMappingService(IMenuMappingRepository menuMappingRepository)
        {
            _menuMappingRepository = menuMappingRepository;
        }
        public async Task<List<ModelUserMenuMapping>> GetAllMenuMappingAsync()
        {
            return await _menuMappingRepository.GetAllMenuMappingAsync();
        }

        public async Task<string> AddMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            return await _menuMappingRepository.AddMenuMappingAsync(modelUserMenuMapping);
        }
        public async Task<string> UpdateMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            return await _menuMappingRepository.UpdateMenuMappingAsync(modelUserMenuMapping);
        }
        public async Task<string> DeleteMenuMappingAsync(int userId)
        {
            return await _menuMappingRepository.DeleteMenuMappingAsync(userId);
        }
        public async Task<ModelUserMenuMapping> GetMenuMappingByIdAsync(int userId)
        {
            return await _menuMappingRepository.GetMenuMappingByIdAsync(userId);
        }
    }
}
