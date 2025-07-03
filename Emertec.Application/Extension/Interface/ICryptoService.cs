using MicroService_Template.Application.DTO;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface ICryptoService
    {
        KeyGenerationResponse GenerateRsaKeys(string outputFolder);
    }
}
