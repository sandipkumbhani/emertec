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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MicroService_Template.Application.Services
{
    public class ModelUserMenuMappingService : IModelUserMenuMappingService
    {
        private readonly IModelUserMenuMappingRepository _modelUserMenuMappingRepository;
        public ModelUserMenuMappingService(IModelUserMenuMappingRepository modelUserMenuMappingRepository)
        {
            _modelUserMenuMappingRepository = modelUserMenuMappingRepository;
        }
        public async Task<List<ModelUserMenuMapping>> CreateMenuMasterMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            string[] MenuId = modelUserMenuMapping.MenuIds.Split(',');
            IList<ModelUserMenuMapping> ModelUserMenuMappingList = new List<ModelUserMenuMapping>();
            foreach (var item in MenuId)
            {
                ModelUserMenuMappingList.Add(new ModelUserMenuMapping()
                {
                    MenuId = Convert.ToInt32(item),
                    UserId = modelUserMenuMapping.UserId,
                    IsActive = true,
                    InsertBy = 1,
                    InsertDate = DateTime.Now,
                    UpdateBy = 1,
                    UpdateDate = DateTime.Now
                });
            }

            return await _modelUserMenuMappingRepository.AddMenuMappingAsync(ModelUserMenuMappingList);
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
            await _modelUserMenuMappingRepository.DeleteMenuMappingAsync(deleteMenu.Result);
        }
        public async Task<ModelUserMenuMapping> UpdateMenuMappingAsync(int userId, ModelUserMenuMapping modelUserMenuMapping)
        {

            var menuMasterMappingExisting = await _modelUserMenuMappingRepository.GetMenuMappingById(userId);

            if (menuMasterMappingExisting == null)
            {
                throw new Exception($"Menu Mapping not found for userId: {userId}");
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
        public async Task<ModelUserMenuMapping> GetMenuMappingDetailsById(int userId)
        {
            var menuDetails = await _modelUserMenuMappingRepository.GetMenuMappingById(userId);
            if (menuDetails == null)
            {
                throw new KeyNotFoundException($"Menu Mapping Id with ID {userId} not found.");
            }

            return new ModelUserMenuMapping
            {
                UserMenuMappingId = menuDetails.UserMenuMappingId,
                UserId = menuDetails.UserId,
                MenuIds = menuDetails.MenuIds,
                IsActive = menuDetails.IsActive,
                InsertBy = menuDetails.InsertBy,
                InsertDate = menuDetails.InsertDate,
                UpdateBy = menuDetails.UpdateBy,
                UpdateDate = menuDetails.UpdateDate,
                User = menuDetails.User



            };
        }
    }
}
