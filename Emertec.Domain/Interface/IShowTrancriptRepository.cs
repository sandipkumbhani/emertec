using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IShowTrancriptRepository
    {
        Task<List<ModelDimJson>> GetFileNameByIsTranscriptAsync();
        Task<ModelDimJson?> GetByFileNameAsync(string fileName);
        Task<bool> MarFileAsTranscriptedAsync(string fileName, long loggedInUserId);
        Task<List<ModelDimJson>> GetFileNameByIsTranscriptTrueAsync();
    }
}
