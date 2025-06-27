using MicroService_Template.Application.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IConvertRsaToJsonService
    {
        
        List<string> WorkerMp3ToJson(DecryptRequest request, string privateKeyPath, string whisperExePath);

       
       
    }
}
