using MicroService_Template.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IAudioFileService
    {
        List<string> ConvertAllMp3FilesToRsaAndGuid(AudioPaths _paths);
       
     }
}
