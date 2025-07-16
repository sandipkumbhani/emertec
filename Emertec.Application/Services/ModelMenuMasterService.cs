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
        public async Task<ModelMenuMaster> CreateMenuMasterAsync(MenuMasterDTO menuMasterDTO)
        {
            var menuMaster = new ModelMenuMaster
            {
                Name = menuMasterDTO.Name,
                Description = menuMasterDTO.Description,
                Icon = menuMasterDTO.Icon,
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
               Icon = menuMaster.Icon,
               IsActive = menuMaster.IsActive,
                InsertBy = menuMaster.InsertBy,
                InsertDate = menuMaster.InsertDate,
                UpdateBy = menuMaster.UpdateBy,
                UpdateDate = menuMaster.UpdateDate,


            }).ToList();
        }

    }
}
