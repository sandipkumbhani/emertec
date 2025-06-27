using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelDimJsonRepository
    {
        Task InsertJsonRecordAsync(ModelDimJson model);
        Task<ModelDimJson?> GetByDapperGuidAsync(Guid guid);
        Task UpdateAsync(ModelDimJson model);
        Task SaveChangesAsync();
        

    }
}
