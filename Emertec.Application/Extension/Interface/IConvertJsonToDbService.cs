using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;

namespace MicroService_Template.Domain.Extension.Interface
{
    public interface IConvertJsonToDbService
    {
        Task<List<ModelDimJson>> SaveJsonToDB(JsonToDbDTO jsonToDb);
    }
}
