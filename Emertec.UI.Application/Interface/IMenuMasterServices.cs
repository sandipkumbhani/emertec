
using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Application.Interface
{
    public interface IMenuMasterServices
    {
        Task<List<ModelMenuMaster>> GetAllMenuMasterAsync();
        Task<ModelMenuMaster?> GetMenuByIdAsync(int menuId);
        Task<string> AddMenuAsync(ModelMenuMaster modelMenuMaster);
        Task<string> UpdateMenuAsync(ModelMenuMaster modelMenuMaster);
        Task<string> DeleteMenuAsync(int menuId);
    }
}
