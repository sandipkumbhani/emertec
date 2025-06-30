using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelDimTextWordRepositorycs
    {

        Task<ModelDimJson?> GetByGuidAsync(Guid guid);

        Task<ModelDimTextSentence?> GetbyTextSentenceIdAsync(Guid guid);

        Task InsertAsync(ModelDimWord word);
        Task SaveChangesAsync();
    }
}
