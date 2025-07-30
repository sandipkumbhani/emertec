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
        Task<List<ModelDimJson>> GetFileNamesByIsTranscriptAsync();
        Task<ModelDimJson?> GetByFileNameAsync(string fileName);
    }
}
