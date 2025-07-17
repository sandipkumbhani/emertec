using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;

namespace MicroService_Template.Application.Services
{
    public class ModelMenuMasterService : IModelMenuMasterService
    {
        private readonly IModelMenuMasterRepository _modelMenuMasterRepository;
        public ModelMenuMasterService(IModelMenuMasterRepository modelMenuMasterRepository)
        {
            _modelMenuMasterRepository = modelMenuMasterRepository;
        }
        public async Task<ModelMenuMaster> CreateMenuMasterAsync(ModelMenuMaster modelMenuMaster)
        {
            var menuMaster = new ModelMenuMaster
            {
                Name = modelMenuMaster.Name,
                Description = modelMenuMaster.Description,
                Icon = modelMenuMaster.Icon,
                IsActive = true,
                InsertBy = 1, 
                InsertDate = DateTime.Now,
                UpdateBy = 1,
                UpdateDate = DateTime.Now
            };

            return await _modelMenuMasterRepository.AddMenuMasterAsync(menuMaster);
        }
        public async Task<List<ModelMenuMaster>> GetModelMenuMastersAsync()
        {
            var users = await _modelMenuMasterRepository.GetAllMenuAsync();

            return users.Select(menuMaster => new ModelMenuMaster
            {
               MenuId = menuMaster.MenuId,
               Description= menuMaster.Description,
                Name = menuMaster.Name,
                Icon = menuMaster.Icon,
               IsActive = menuMaster.IsActive,
                InsertBy = menuMaster.InsertBy,
                InsertDate = menuMaster.InsertDate,
                UpdateBy = menuMaster.UpdateBy,
                UpdateDate = menuMaster.UpdateDate,


            }).ToList();
        }
        public async Task DeleteMenuById(int id)
        {
            var deleteMenu = _modelMenuMasterRepository.GetMenuById(id);
            if (deleteMenu == null)
            {
                throw new KeyNotFoundException($"Menu Master ID {id} not found.");
            }

            await _modelMenuMasterRepository.DeleteMenuAsync(deleteMenu);
        }
        public async Task<ModelMenuMaster> UpdateMenuAsync(int menuid, ModelMenuMaster modelMenuMaster)
        {

            var menuExisting = _modelMenuMasterRepository.GetMenuById(menuid);

            if (menuExisting == null)
            {
                throw new Exception($"Menu Master with ID {menuid} not found.");
            }
            menuExisting.Name = modelMenuMaster.Name;
            menuExisting.Description = modelMenuMaster.Description;
            menuExisting.Icon = modelMenuMaster.Icon;
            menuExisting.IsDefault = true;
            menuExisting.IsActive = true;
            menuExisting.InsertBy = 1;
            menuExisting.InsertDate = DateTime.UtcNow;
            menuExisting.UpdateBy = 1;
            menuExisting.UpdateDate = DateTime.UtcNow;


           await _modelMenuMasterRepository.UpdatMenuAsync(menuExisting);

            return menuExisting;
        }

    }
}
