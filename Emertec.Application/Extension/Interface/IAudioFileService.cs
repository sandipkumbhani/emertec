using MicroService_Template.Application.DTO;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IAudioFileService
    {
        List<string> ConvertAllMp3FilesToRsaAndGuid(AudioPathsDTO _paths);
       
     }
}
