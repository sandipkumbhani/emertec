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
        Task<List<string>> GetSentencesByTelephoneAsync(string telephoneNumber);
    }
}
