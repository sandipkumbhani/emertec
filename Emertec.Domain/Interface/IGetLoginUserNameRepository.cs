using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IGetLoginUserNameRepository
    {
        Task<ModelUsers> GetUserNameAsync(long userid);
    }
}
