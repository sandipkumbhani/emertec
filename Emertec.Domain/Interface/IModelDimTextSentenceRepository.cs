using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelDimTextSentenceRepository
    {
        Task<ModelDimJson?> GetByDapperGuidAsync(Guid guid);
        Task InsertAsync(ModelDimTextSentence sentence);
        Task<bool> ExistsByJsonGuidAsync(Guid jsonGuid);
        Task SaveChangesAsync();
    }
}
