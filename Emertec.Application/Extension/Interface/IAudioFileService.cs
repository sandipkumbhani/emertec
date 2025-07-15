using MicroService_Template.Domain.DTO;

namespace MicroService_Template.Domain.Extension.Interface
{
    public interface IAudioFileService
    {
        List<string> ConvertAllMp3FilesToRsaAndGuid(AudioPathsDTO _paths);
       
     }
}
