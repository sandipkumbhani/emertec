
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
        Task<string> AddMenuAsync(ModelMenuMaster modelMenuMaster);
    }
}
