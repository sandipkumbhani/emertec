using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using System.Data;

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
        public async Task DeleteMenuMappingById(int userId)
        {
            var deleteMenu = _modelUserMenuMappingRepository.GetMenuMappingById(userId);
            if (deleteMenu == null)
            {
                throw new KeyNotFoundException($"User ID {userId} not found.");
            }
            await _modelUserMenuMappingRepository.DeleteMenuMappingAsync(deleteMenu.Result);
        }
        public async Task<ModelUserMenuMapping> UpdateMenuMappingAsync(int userId, ModelUserMenuMapping modelUserMenuMapping)
        {
            var existingMappings = await _modelUserMenuMappingRepository.GetAllActiveMappingsByUserIdAsync(userId);
            var existingMenuIds = existingMappings.Select(x => x.MenuId).ToList();

            var newMenuIds = modelUserMenuMapping.MenuIds?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(id => Convert.ToInt64(id.Trim()))
                .Distinct()
                .ToList() ?? new List<long>();

            var menuIdsToAdd = newMenuIds.Except(existingMenuIds).ToList();
            var menuIdsToRemove = existingMenuIds.Except(newMenuIds).ToList();

            var newMappings = menuIdsToAdd.Select(menuId => new ModelUserMenuMapping
            {
                UserId = userId,
                MenuId = menuId,
                IsActive = true,
                InsertBy = 1,
                InsertDate = DateTime.UtcNow,
                UpdateBy = 1,
                UpdateDate = DateTime.UtcNow
            }).ToList();

            if (newMappings.Any())
                await _modelUserMenuMappingRepository.AddMenuMappingAsync(newMappings);

            foreach (var menuId in menuIdsToRemove)
            {
                var mappingToRemove = existingMappings.FirstOrDefault(x => x.MenuId == menuId);
                if (mappingToRemove != null)
                {
                    mappingToRemove.IsActive = false;
                    mappingToRemove.UpdateBy = 1;
                    mappingToRemove.UpdateDate = DateTime.UtcNow;
                    await _modelUserMenuMappingRepository.UpdatMenuMappingAsync(mappingToRemove);
                }
            }
            return new ModelUserMenuMapping
            {
                UserId = userId,
                MenuIds = string.Join(",", newMenuIds)
            };
        }

        public async Task<ModelUserMenuMapping> GetMenuMappingDetailsById(int userId)
        {

            var menuDetails = await _modelUserMenuMappingRepository.GetMenuMappingById(userId);
            if (menuDetails == null)
            {
                throw new KeyNotFoundException($"Menu Mapping Id with ID {userId} not found.");
            }
            return menuDetails;
            //return new ModelUserMenuMapping
            //{
            //    UserMenuMappingId = menuDetails.UserMenuMappingId,
            //    UserId = menuDetails.UserId,
            //    MenuIds = menuDetails.MenuIds,
            //    IsActive = menuDetails.IsActive,
            //    InsertBy = menuDetails.InsertBy,
            //    InsertDate = menuDetails.InsertDate,
            //    UpdateBy = menuDetails.UpdateBy,
            //    UpdateDate = menuDetails.UpdateDate,
            //    User = menuDetails.User

            //};
        }
    }
}
