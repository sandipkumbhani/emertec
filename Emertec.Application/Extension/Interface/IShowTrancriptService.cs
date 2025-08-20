using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IShowTrancriptService
    {
        Task<List<ModelDimJson>> GetFileNameByIsTranscript();
        Task<List<string>> GetByFileNameAsync(string filename);
        Task<bool> MarFileAsTranscriptedAsync(string fileName, long loggedInUserId);
        Task<List<ModelDimJson>> GetFileNameByIsTranscriptTrue();
    }
}
