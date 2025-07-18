using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Application.Interface
{
    public interface IShowTranscriptServices
    {
        Task<ModelDimJson> GetTranscriptsByTelephoneNoAsync(string Telephoneno);
    }
}
