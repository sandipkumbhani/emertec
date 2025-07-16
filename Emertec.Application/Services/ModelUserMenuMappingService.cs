using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
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
            var username= await _modelUserMenuMappingRepository.GetUserWithRoleAsync(modelUserMenuMapping.UserId);

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

            return await _modelUserMenuMappingRepository.AddMenuMasterMappingAsync(menuMasterMapping);
        }
        public async Task<List<ModelUserMenuMapping>> GetAllMenuMappingAsync()
        {
            var users = await _modelUserMenuMappingRepository.GetAllMenuMapping();

            return users.Select(user => new ModelUserMenuMapping
            {
                UserId= user.UserId,
                MenuId = user.MenuId,
                IsActive = user.IsActive,
                InsertBy = user.InsertBy,
                InsertDate = user.InsertDate,
                UpdateBy = user.UpdateBy,
                UpdateDate = user.UpdateDate,

            }).ToList();
        }
    }
}
