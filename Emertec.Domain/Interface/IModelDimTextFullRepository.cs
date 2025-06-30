using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IModelDimTextFullRepository
    {
        Task<ModelDimJson?> GetByJsonidAsync(Guid guid);
        Task InsertAsync(ModelDimTextFull textFull);
       Task<ModelDimTextFull?> GetByJsonGuidAsync(Guid jsonGuid);
        Task SaveChangesAsync();
    }
}
