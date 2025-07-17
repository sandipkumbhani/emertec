using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Services
{
    public class ModelUserMenuMappingService : IModelUserMenuMappingService
    {
        private readonly IModelUserMenuMappingRepository _modelUserMenuMappingRepository;
        public ModelUserMenuMappingService(IModelUserMenuMappingRepository modelUserMenuMappingRepository)
        {
            _modelUserMenuMappingRepository = modelUserMenuMappingRepository;
        }
        public async Task<ModelUserMenuMapping> CreateMenuMasterMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            var menuMasterMapping = new ModelUserMenuMapping
            {
                
                MenuId = modelUserMenuMapping.MenuId,
                UserId = modelUserMenuMapping.UserId,
                IsActive = true,
                InsertBy = 1,
                InsertDate = DateTime.Now,
                UpdateBy = 1,
                UpdateDate = DateTime.Now
            };

            return await _modelUserMenuMappingRepository.AddMenuMappingAsync(menuMasterMapping);
        }
        public async Task<List<ModelUserMenuMapping>> GetAllMenuMappingAsync()
        {
            var users = await _modelUserMenuMappingRepository.GetAllMenuMapping();

            return users.Select(user => new ModelUserMenuMapping
            {
                UserMenuMappingId = user.UserMenuMappingId,
                UserId = user.UserId,
                MenuId = user.MenuId,
                IsActive = user.IsActive,
                InsertBy = user.InsertBy,
                InsertDate = user.InsertDate,
                UpdateBy = user.UpdateBy,
                UpdateDate = user.UpdateDate,
                User = user.User,
                Menu = user.Menu



            }).ToList();
        }
        public async Task DeleteMenuMappingById(int id)
        {
            var deleteMenu = _modelUserMenuMappingRepository.GetMenuMappingById(id);
            if (deleteMenu == null)
            {
                throw new KeyNotFoundException($"User ID {id} not found.");
            }

            await _modelUserMenuMappingRepository.DeleteMenuMappingAsync(deleteMenu);
        }
        public async Task<ModelUserMenuMapping> UpdateMenuMappingAsync(int UserMenuMappingId, ModelUserMenuMapping modelUserMenuMapping)
        {

            var menuMasterMappingExisting = _modelUserMenuMappingRepository.GetMenuMappingById(UserMenuMappingId);

            if (menuMasterMappingExisting == null)
            {
                throw new Exception($"Menu with ID {UserMenuMappingId} not found.");
            }
            menuMasterMappingExisting.MenuId = modelUserMenuMapping.MenuId;
            menuMasterMappingExisting.UserId = modelUserMenuMapping.UserId;
            menuMasterMappingExisting.IsActive = true;
            menuMasterMappingExisting.InsertBy = 1;
            menuMasterMappingExisting.InsertDate = DateTime.UtcNow;
            menuMasterMappingExisting.UpdateBy = 1;
            menuMasterMappingExisting.UpdateDate = DateTime.UtcNow;
          
            await _modelUserMenuMappingRepository.UpdatMenuMappingAsync(menuMasterMappingExisting);

            return menuMasterMappingExisting;
        }
    }
}
