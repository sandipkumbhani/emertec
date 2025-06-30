using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelDimCompanyRepository
    {
        Task<ModelDimCompany> GetByNameAsync(string name);
        Task companyInsertAsync(ModelDimCompany company);
        Task<ModelDimCompany?> GetByCompanyIdAsync(Guid guid);
        Task SaveChangesAsync();
    }
}
