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
    public class ModelUserRoleService : IModelUserRoleService
    {
        private readonly IModelUserRoleRepository _modelUserRoleRepository;

        public ModelUserRoleService(IModelUserRoleRepository modelUserRoleRepository)
        {
            _modelUserRoleRepository = modelUserRoleRepository;
        }
        public async Task<ModelUserRole> CreateUserRoleAsync(ModelUserRole modelUserRole)
        {
            var menuMaster = new ModelUserRole
            {
               Name = modelUserRole.Name,
                IsActive = true,
                InsertBy = 1,
                InsertDate = DateTime.Now,
                UpdateBy = 1,
                UpdateDate = DateTime.Now
            };

            return await _modelUserRoleRepository.AddUserRoleAsync(menuMaster);
        }
        public async Task<List<ModelUserRole>> GetAllUsersRoleAsync()
        {
            var users = await _modelUserRoleRepository.GetAllUsersRole();

            return users.Select(user => new ModelUserRole
            {
                UserRoleId = user.UserRoleId,
                Name = user.Name,
                IsActive = user.IsActive,
                InsertBy = user.InsertBy,
                InsertDate = user.InsertDate,
                UpdateBy = user.UpdateBy,
                UpdateDate = user.UpdateDate,
               
            }).ToList();
        }
        public async Task DeleteUserRoleById(int roleid)
        {
            var deleteUserRole = _modelUserRoleRepository.GetUserRoleById(roleid);
            if (deleteUserRole == null)
            {
                throw new KeyNotFoundException($"UserRole ID {roleid} not found.");
            }

            await _modelUserRoleRepository.DeleteRoleAsync(deleteUserRole);
        }
        public async Task<ModelUserRole> UpdateUserRoleAsync(int roleid, ModelUserRole modelUserRole)
        {

            var userRoleExisting = _modelUserRoleRepository.GetUserRoleById(roleid);

            if (userRoleExisting == null)
            {
                throw new Exception($"UserRole with ID {roleid} not found.");
            }
            userRoleExisting.Name = modelUserRole.Name;
            userRoleExisting.IsActive = true;
            userRoleExisting.InsertBy = 1;
            userRoleExisting.InsertDate = DateTime.UtcNow;
            userRoleExisting.UpdateBy = 1;
            userRoleExisting.UpdateDate = DateTime.UtcNow;


            await _modelUserRoleRepository.UserRoleUpdateAsync(userRoleExisting);

            return userRoleExisting;
        }
    }
}
