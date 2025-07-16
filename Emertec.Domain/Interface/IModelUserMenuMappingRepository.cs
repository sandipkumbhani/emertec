using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelUserMenuMappingRepository
    {
        Task<ModelUserMenuMapping> AddMenuMasterMappingAsync(ModelUserMenuMapping modelUserMenuMapping);
        Task<List<ModelUserMenuMapping>> GetAllMenuMapping();
        Task<ModelUsers?> GetUserWithRoleAsync(long userId);


    }
}
