
using Emertec.UI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Application.Interface
{
    public interface ILoginServices
    {
        Task<string> Login(LoginViewModel model);
    }
}

