using MicroService_Template.Domain.DTO;

namespace MicroService_Template.Domain.Extension.Interface
{
    public interface ICryptoService
    {
        KeyGenerationResponseDTO GenerateRsaKeys(string outputFolder);
    }
}
