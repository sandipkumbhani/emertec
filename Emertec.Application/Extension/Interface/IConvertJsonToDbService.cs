using MicroService_Template.Application.DTO;
using MicroService_Template.Domain.Model;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IConvertJsonToDbService
    {
        Task<List<ModelDimJson>> CheckGuidFromJsonAsync(JsonToDB jsonToDb);
    }
}
